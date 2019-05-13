namespace Model.DTO.MsSql
{
    public class WC_CrowdDTO
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string shopName { get; set; }

        /// <summary>
        /// 开始抽奖时间
        /// </summary>
        public string lotteryTimeStr { get; set; }

        /// <summary>
        /// 抽奖时间状态(1:未开始;2:进行中;3:已结束)
        /// </summary>
        public int lotteryTimeStatus { get; set; }
    }
}
