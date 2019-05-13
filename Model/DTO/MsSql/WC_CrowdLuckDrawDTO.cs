namespace Model.DTO.MsSql
{
    public class WC_CrowdLuckDrawDTO
    {
        /// <summary>
        /// 群名称
        /// </summary>
        public int luckDrawTime { get; set; }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string userMobile { get; set; }

        /// <summary>
        /// 是否为本人
        /// </summary>
        public int isSelf { get; set; }

        /// <summary>
        /// 抽奖时间
        /// </summary>
        public string createTimeSpan { get; set; }

        /// <summary>
        /// 会员电话
        /// </summary>
        public string phone { get; set; }
    }
}
