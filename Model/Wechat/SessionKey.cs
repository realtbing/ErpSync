namespace Model.Wechat
{
    public class SessionKey
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 用户相对于小程序唯一标示
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 会话密钥
        /// </summary>
        public string session_key { get; set; }

        /// <summary>
        /// 用户在开放平台的唯一标识符。本字段在满足一定条件的情况下才返回
        /// </summary>
        public string unionid { get; set; }
    }
}
