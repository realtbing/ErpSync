using System;

namespace Model.Entities.MsSql
{
    public class WC_CrowdLuckDraw
    {
        public int cldid { get; set; }

        public string shopCode { get; set; }

        public string shopName { get; set; }

        public string openGid { get; set; }

        public string groupName { get; set; }

        public string code { get; set; }

        public string openId { get; set; }

        public string userName { get; set; }

        public string headPicture { get; set; }

        public int luckyNumber { get; set; }

        public int winning { get; set; }

        public string date { get; set; }

        public DateTime createTime { get; set; }

        public DateTime? winnerTime { get; set; }
    }
}
