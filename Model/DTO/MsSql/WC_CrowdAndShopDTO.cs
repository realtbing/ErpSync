using System;

namespace Model.DTO.MsSql
{
    public class WC_CrowdAndShopDTO : WC_CrowdDTO
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string openGId { get; set; }

        /// <summary>
        /// 开始抽奖时间
        /// </summary>
        public DateTime lotteryTime { get; set; }
    }
}
