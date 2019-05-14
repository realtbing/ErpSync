using System;

namespace Logic.Wechat
{
    /// <summary>
    /// 微信异常
    /// </summary>
    public class WxPayException : Exception
    {
        public WxPayException(string msg) : base(msg)
        {

        }
    }
}
