using System.Collections.Generic;

namespace Model.DTO.MsSql
{
    public class WC_CrowdLuckDrawHistoryResultDTO
    {
        /// <summary>
        /// 群名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 抽奖日期
        /// </summary>
        public string lotteryDate { get; set; }

        /// <summary>
        /// 抽奖数
        /// </summary>
        public int lotteryCount { get; set; }

        /// <summary>
        /// 抽奖时间(共花费时间)
        /// </summary>
        public int lotteryTime { get; set; }

        /// <summary>
        /// 抽奖记录
        /// </summary>
        public List<WC_CrowdLuckDrawDTO> lotteryRecord { get; set; }
    }
}
