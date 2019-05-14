namespace Model.Appsettings
{
    public class Appsetting
    {
        /// <summary>
        /// 是否检查抽奖用户与群的关系
        /// </summary>
        public bool IsCheckUserAndCrowdRelation { get; set; }

        /// <summary>
        /// 抽奖时间(时分)
        /// </summary>
        public string LotteryTime { get; set; }

        /// <summary>
        /// 抽奖时间总量(分钟)
        /// </summary>
        public int LotteryMinute { get; set; }

        /// <summary>
        /// 允许抽奖人数
        /// </summary>
        public int AllowLotteryUserCount { get; set; }

        /// <summary>
        /// 允许中奖人数
        /// </summary>
        public int AllowDrawUserCount { get; set; }

        /// <summary>
        /// 群用户数量
        /// </summary>
        public int CrowdUserCount { get; set; }

        #region
        /// <summary>
        /// 微信商户号
        /// </summary>
        public string wechatMchId { get; set; }

        /// <summary>
        /// 微信同步跳转地址
        /// </summary>
        //public string wechatNotifyUrl { get; set; }

        /// <summary>
        /// 微信退款地址
        /// </summary>
        //public string wechatRefundUrl { get; set; }

        /// <summary>
        /// 微信授权认证地址
        /// </summary>
        public string wechatCodeToSessionUrl { get; set; }
        #endregion
    }
}
