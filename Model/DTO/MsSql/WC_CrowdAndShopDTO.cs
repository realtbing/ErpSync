using System;

namespace Model.DTO.MsSql
{
    public class WC_CrowdAndShopDTO : WC_CrowdDTO
    {
        /// <summary>
        /// 群Id
        /// </summary>
        public string openGId { get; set; }

        /// <summary>
        /// 门店编号
        /// </summary>
        public string shopCode { get; set; }

        /// <summary>
        /// 开始抽奖时间
        /// </summary>
        public DateTime lotteryTime { get; set; }

        /// <summary>
        /// 抽奖时间(分钟)
        /// </summary>
        public int lotteryMinute { get; set; }

        /// <summary>
        /// 允许抽奖人数(0:未开始抽奖)
        /// </summary>
        public int allowLotteryPepoleNumber { get; set; }

        /// <summary>
        /// 允许中奖人数(0:未开始抽奖)
        /// </summary>
        public int allowDrawPepoleNumber { get; set; }
    }
}
