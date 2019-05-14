namespace Model.Wechat
{
    public class MobileDecryption
    {
        /// <summary>
        /// 用户绑定的手机号（国外手机号会有区号）
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// 没有区号的手机号
        /// </summary>
        public string purePhoneNumber { get; set; }
        /// <summary>
        /// 区号
        /// </summary>
        public string countryCode { get; set; }
        /// <summary>
        /// 水印
        /// </summary>
        public WaterMark watermark { get; set; }
    }
}
