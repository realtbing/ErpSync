using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Api.Models;
using Foundation;
using Foundation.Encrypt;
using Logic.MsSql;
using Logic.Wechat;
using Model;
using Model.Appsettings;
using Model.DTO.MsSql;
using Model.Entities.MsSql;
using Model.Wechat;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrowdController : ControllerBase
    {
        private readonly IOptions<Appsetting> _options;
        WC_CrowdLogic wc_CrowdLogic;
        WC_OfficalAccountsLogic wc_OfficalAccountsLogic;
        WC_UserLogic wc_UserLogic;

        public CrowdController(IOptions<Appsetting> options)
        {
            _options = options;
            wc_CrowdLogic = new WC_CrowdLogic(_options);
            wc_OfficalAccountsLogic = new WC_OfficalAccountsLogic();
            wc_UserLogic = new WC_UserLogic();
        }

        /// <summary>
        /// 群信息
        /// </summary>
        /// <param name="encryptType"></param>
        /// <param name="iv">加密算法初始向量</param>
        /// <param name="encryptedData">微信加密数据</param>
        /// <param name="code">微信登录凭证code</param>
        /// <param name="appId">小程序appid</param>
        /// <param name="openId">用户OpenId</param>
        /// <param name="openGid">群Id</param>
        /// <returns></returns>
        [HttpGet("encryptType={encryptType}&iv={iv}&encryptedData={encryptedData}&code={code}&appId={appid}&&openid={openId}&opengid={openGid}")]
        //[HttpGet]
        public ReturnModel<WC_CrowdDTO> Get(string encryptType, string iv, string encryptedData, string code, string appId, string openId, string openGid)
        {
            ReturnModel<WC_CrowdDTO> result = new ReturnModel<WC_CrowdDTO>();

            #region 参数检查
            if (string.IsNullOrEmpty(encryptType))
            {
                result.Type = ResultType.Error;
                result.Message = "Param:encryptType is null";
                return result;
            }
            if (string.IsNullOrEmpty(iv))
            {
                result.Type = ResultType.Error;
                result.Message = "Param:iv is null";
                return result;
            }
            if (string.IsNullOrEmpty(encryptedData))
            {
                result.Type = ResultType.Error;
                result.Message = "Param:encryptedData is null";
                return result;
            }
            if (string.IsNullOrEmpty(appId))
            {
                result.Type = ResultType.Error;
                result.Message = "Param:appId is null";
                return result;
            }
            #endregion

            #region
            WC_User userEntity = null;
            if (string.IsNullOrEmpty(openGid))
            {

                #region 微信配置信息
                WC_OfficalAccounts officalAccountEntity = wc_OfficalAccountsLogic.GetSingle(appId);
                if (officalAccountEntity == null)
                {
                    result.Type = ResultType.Error;
                    result.Message = "Wechat configuration not found";
                    return result;
                }
                #endregion
                string _session_key = string.Empty;
                if (string.IsNullOrEmpty(code))
                {
                    if (string.IsNullOrEmpty(openId))
                    {
                        result.Type = ResultType.Error;
                        result.Message = "Param:openId is null";
                        return result;
                    }
                    //未过期
                    userEntity = wc_UserLogic.GetSingle(openId);
                    if (userEntity == null || string.IsNullOrEmpty(userEntity.SessionKey))
                    {
                        result.Type = ResultType.Error;
                        result.Message = string.Format("OpenId: {0}, not found", openId);
                        return result;
                    }
                    _session_key = userEntity.SessionKey;
                }
                else
                {
                    WxPayData inputData = new WxPayData();
                    inputData.SetValue("appid", officalAccountEntity.AppId);
                    inputData.SetValue("secret", officalAccountEntity.AppSecret);
                    inputData.SetValue("js_code", code);
                    WxPayData json_object = WxPayApi.CodeToSession(inputData);
                    if (json_object.IsSet("DataResult"))
                    {
                        string _json_data = json_object.GetValue("DataResult").ToString();
                        var session = JsonConvert.DeserializeObject<SessionKey>(_json_data);
                        if (session != null && !string.IsNullOrEmpty(session.session_key))
                        {
                            int _errorCode = session.errcode;
                            string _errorMsg = session.errmsg;
                            //有效的情况
                            if (_errorCode == 0)
                            {
                                _session_key = session.session_key;
                                openId = session.openid;
                                userEntity = wc_UserLogic.GetSingle(openId);
                                //特殊参数
                                if (userEntity == null)
                                {
                                    userEntity = new WC_User();
                                    userEntity.Id = Snowflake.Instance().GetId().ToString();
                                    userEntity.OpenId = openId;
                                    userEntity.Sex = 1;
                                    userEntity.SubscribeTime = DateTime.Now;
                                    userEntity.Subscribe = 1;
                                    userEntity.OfficalAccountId = officalAccountEntity.Id;
                                    userEntity.UnionId = session.unionid;
                                    userEntity.SessionKey = _session_key;
                                    wc_UserLogic.Add(userEntity);
                                }
                                else
                                {
                                    userEntity.SessionKey = _session_key;
                                    wc_UserLogic.Update(userEntity);
                                }
                            }
                            else
                            {
                                result.Type = ResultType.Error;
                                result.Message = _errorMsg;
                                return result;
                            }
                        }
                        else
                        {
                            result.Type = ResultType.Error;
                            result.Message = "Wechat grant authorization failure";
                            return result;
                        }
                    }
                    else
                    {
                        result.Type = ResultType.Error;
                        result.Message = "Wechat grant authorization failure";
                        return result;
                    }
                }

                //解密
                string _encrypted_data = AESHelper.AESDecrypt(encryptedData, _session_key, iv);
                var et = (EncryptType)Enum.Parse(typeof(EncryptType), encryptType);
                switch (et)
                {
                    case EncryptType.User:
                        UserDecryption userInfo = JsonConvert.DeserializeObject<UserDecryption>(_encrypted_data);
                        if (userInfo != null)
                        {
                            if (!userInfo.openId.Equals(openId))
                            {
                                result.Type = ResultType.Error;
                                result.Message = "OpenId not equal";
                                return result;
                            }
                            userEntity.City = userInfo.city;
                            userEntity.NickName = userInfo.nickName;
                            userEntity.Language = "zh_CN";
                            userEntity.Province = userInfo.province;
                            userEntity.Country = userInfo.country;
                            userEntity.HeadImgUrl = userInfo.avatarUrl;
                            userEntity.Sex = userInfo.gender;
                            userEntity.UnionId = userInfo.unionId;
                            wc_UserLogic.Update(userEntity);
                        }
                        break;
                    case EncryptType.OpenGid:
                        var groupId = JsonConvert.DeserializeObject<string>(_encrypted_data);
                        if (string.IsNullOrEmpty(groupId))
                        {
                            result.Type = ResultType.Error;
                            result.Message = "Not found openGid";
                            return result;
                        }
                        else
                        {
                            openGid = groupId;
                            var crowdEntity = wc_CrowdLogic.GetSingle(openGid);
                            if (crowdEntity == null)
                            {
                                crowdEntity = new WC_Crowd();
                                crowdEntity.openGId = groupId;
                                crowdEntity.name = "";
                                crowdEntity.shopCode = "";
                                crowdEntity.status = 1;
                                crowdEntity.lotteryTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd ") + _options.Value.LotteryTime);
                                crowdEntity.joinPeople = _options.Value.AllowLotteryUserCount;
                                crowdEntity.winners = _options.Value.AllowDrawUserCount;
                                crowdEntity.createTime = DateTime.Now;
                                crowdEntity.operatorStaff = "系统";
                                crowdEntity.lastTime = DateTime.Now;
                                wc_CrowdLogic.Add(crowdEntity);
                            }
                            else
                            {
                                crowdEntity.lotteryTime = crowdEntity.lotteryTime.HasValue ? (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd ") + crowdEntity.lotteryTime.Value.ToString("HH:mm:ss"))) : Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd ") + _options.Value.LotteryTime);
                            }
                        }
                        break;
                }
            }

            var returnDto = wc_CrowdLogic.GetSingle(openId, openGid);
            if (returnDto == null)
            {
                result.Type = ResultType.Error;
                result.Message = "object is not exist";
            }
            else
            {
                result.Result = returnDto;
            }
            return result;
            #endregion
        }
    }
}