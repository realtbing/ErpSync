using System;

namespace Model.Entities.MsSql
{
    public class WC_Crowd
    {
        public int cid { get; set; }

        public string openGId { get; set; }

        public string name { get; set; }

        public string shopCode { get; set; }

        public int status { get; set; }

        public DateTime? lotteryTime { get; set; }

        public int joinPeople { get; set; }

        public int winners { get; set; }

        public DateTime createTime { get; set; }

        public string operatorStaff { get; set; }

        public DateTime lastTime { get; set; }
    }
}
