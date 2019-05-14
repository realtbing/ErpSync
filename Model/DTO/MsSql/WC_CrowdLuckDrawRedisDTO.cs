using System;

namespace Model.DTO.MsSql
{
    public class WC_CrowdLuckDrawRedisDTO
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        public string shopCode { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string shopName { get; set; }

        /// <summary>
        /// 群Id
        /// </summary>
        public string openGid { get; set; }

        /// <summary>
        /// 群名称
        /// </summary>
        public string groupName { get; set; }

        /// <summary>
        /// code(未使用,guid填充)
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 用户OpenId
        /// </summary>
        public string openId { get; set; }

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
        public string luckyNumber { get; set; }

        /// <summary>
        /// 中奖标识(1:中奖;2:未中奖)
        /// </summary>
        public int winning { get; set; }

        /// <summary>
        /// 抽奖时分
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// 开始抽奖时间
        /// </summary>
        public DateTime createTime { get; set; }

        /// <summary>
        /// 抽奖时间(分钟)
        /// </summary>
        public DateTime? winnerTime { get; set; }
    }
}
