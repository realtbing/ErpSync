using System;

namespace Model.Entities.MsSql
{
    public class WC_CrowdUser
    {
        public int cuid { get; set; }

        public string openGid { get; set; }

        public string code { get; set; }

        public string openId { get; set; }

        public string userName { get; set; }

        public string headPicture { get; set; }

        public int status { get; set; }

        public DateTime createTime { get; set; }

        public DateTime? exitTime { get; set; }
    }
}
