using System.Collections.Generic;

namespace Model.DTO.MsSql
{
    public class WC_CrowdLuckDrawResultDTO
    {
        /// <summary>
        /// 群名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 抽奖号码
        /// </summary>
        public int lotteryNumber { get; set; }

        /// <summary>
        /// 抽奖数
        /// </summary>
        public int lotteryCount { get; set; }

        /// <summary>
        /// 抽奖记录
        /// </summary>
        public List<WC_CrowdLuckDrawDTO> lotteryRecord { get; set; }
    }
}
