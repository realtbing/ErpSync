using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Api.Models;
using Model.Appsettings;
using Model.DTO.MsSql;
using Logic.MsSql;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WC_CrowdController : ControllerBase
    {
        private readonly IOptions<Appsetting> _options;
        WC_CrowdLogic wc_CrowdLogic = new WC_CrowdLogic();

        public WC_CrowdController(IOptions<Appsetting> options)
        {
            _options = options;
        }

        /// <summary>
        /// 群信息
        /// </summary>
        /// <param name="openId">用户OpenId</param>
        /// <param name="openGid">群Id</param>
        /// <returns></returns>
        [HttpGet("{openId}, {openGid}")]
        public ReturnModel<WC_CrowdDTO> Get(string openId, string openGid)
        {
            ReturnModel<WC_CrowdDTO> result = new ReturnModel<WC_CrowdDTO>();
            var returnDto = wc_CrowdLogic.GetSingle(openId, openGid, _options.Value.IsCheckUserAndCrowdRelation);
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
        }

        ///// <summary>
        ///// 群历史信息
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public MsgModel History()
        //{
        //    string openid = request.Form["openid"];
        //    string groupCode = request.Form["groupCode"];
        //    string openGid = request.Form["openGid"];
        //    string date = request.Form["dateStr"];
        //    int page = request.Form["page"].GetInt();
        //    int rows = request.Form["rows"].GetInt();

        //    return C_BLL.History(openid, groupCode, openGid, date, page, rows);
        //}

        //[HttpPost]
        //public MsgModel Submit()
        //{
        //    string openid = request.Form["openid"];
        //    string groupCode = request.Form["groupCode"];
        //    string openGid = request.Form["openGid"];

        //    return C_BLL.Submit(openid, groupCode, openGid);
        //}
    }
}