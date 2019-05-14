using System;

namespace Model.DTO.MsSql
{
    public class WC_CrowdLuckDrawDTO
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string headPicture { get; set; }

        /// <summary>
        /// 抽奖号码
        /// </summary>
        public int luckyNumber { get; set; }

        /// <summary>
        /// 中奖标识(1:中奖;2:未中奖)
        /// </summary>
        public int winning { get; set; }

        /// <summary>
        /// 抽奖时分
        /// </summary>
        public string time { get; set; }
    }
}
