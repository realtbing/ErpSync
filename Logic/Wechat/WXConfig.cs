using System;
using Microsoft.Extensions.Options;
using Model.Appsettings;

namespace Logic.Wechat
{
    /// <summary>
    /// 微信基本信息配置实现类
    /// </summary>
    public class WXConfig : IConfig
    {
        private readonly IOptions<Appsetting> _options;
        public WXConfig(IOptions<Appsetting> options)
        {
            _options = options;
        }

        #region
        ///// <summary>
        ///// APPID：绑定支付的APPID（必须配置）
        ///// </summary>
        ///// <returns></returns>
        //public string GetAppID()
        //{
        //    return "";
        //}
        /// <summary>
        /// MCHID：商户号（必须配置）
        /// </summary>
        /// <returns></returns>
        public string GetMchID()
        {
            string _machID = _options.Value.wechatMchId;
            if (String.IsNullOrWhiteSpace(_machID))
            {
                return "1510090631";//婆婆买菜商户号ID
            }
            return _machID;
        }
        ///// <summary>
        ///// KEY：商户支付密钥，参考开户邮件设置（必须配置），请妥善保管，避免密钥泄露
        ///// </summary>
        ///// <returns></returns>
        //public string GetKey()
        //{
        //    return "D53A390F596CF42FAE101C56D52D32B2";
        //}

        ///// <summary>
        ///// APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置），请妥善保管，避免密钥泄露
        ///// </summary>
        ///// <returns></returns>
        //public string GetAppSecret()
        //{
        //    return "";
        //}
        ///// <summary>
        ///// 证书密码
        ///// </summary>
        ///// <returns></returns>
        //public string GetSSlCertPassword()
        //{
        //    return "1510090631";
        //}
        ///// <summary>
        ///// 证书路径
        ///// </summary>
        ///// <returns></returns>
        //public string GetSSlCertPath()
        //{
        //    return "/payment_cert/weixinpayment/p12/apiclient_cert.p12";
        //}

        ///// <summary>
        ///// 支付结果通知回调url，用于商户接收支付结果
        ///// </summary>
        ///// <returns></returns>
        //public string GetNotifyUrl()
        //{
        //    string _notify_url = _options.Value.wechatNotifyUrl;
        //    if (String.IsNullOrWhiteSpace(_notify_url))
        //    {
        //        return "https://api.popomaicai.com/api/wechat/notify.aspx";//婆婆买菜微信通知接口
        //    }
        //    return _notify_url;
        //}

        //public string GetRefundUrl()
        //{
        //    string _refund_url = _options.Value.wechatRefundUrl;
        //    if (String.IsNullOrWhiteSpace(_refund_url))
        //    {
        //        return "https://api.popomaicai.com/api/wechat/refund.aspx";//婆婆买菜微信通知接口
        //    }
        //    return _refund_url;
        //}

        ///// <summary>
        ///// 此参数可手动配置也可在程序中自动获取
        ///// </summary>
        ///// <returns></returns>
        //public string GetIp()
        //{
        //    return "120.79.236.233";
        //}
        ///// <summary>
        ///// 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        ///// </summary>
        ///// <returns></returns>
        //public int GetLogLevel()
        //{
        //    return 1;
        //}
        ///// <summary>
        ///// 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        ///// </summary>
        ///// <returns></returns>
        //public string GetProxyUrl()
        //{
        //    return "";
        //}
        ///// <summary>
        ///// 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        ///// </summary>
        ///// <returns></returns>
        //public int GetReportLevel()
        //{
        //    return 1;
        //}
        #endregion
        /// <summary>
        /// GetCodeToSessionUrl
        /// </summary>
        /// <returns></returns>
        public string GetCodeToSessionUrl()
        {
            string _url = _options.Value.wechatCodeToSessionUrl;
            if (String.IsNullOrWhiteSpace(_url))
            {
                return "https://api.weixin.qq.com/sns/jscode2session";//
            }
            return _url;
        }
    }
}
