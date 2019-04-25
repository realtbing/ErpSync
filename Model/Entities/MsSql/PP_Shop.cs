using System;

namespace Model.Entities.MsSql
{
    public class PP_Shop
    {
        public int shopId { get; set; }

        public string shopCode { get; set; }

        public string shopName { get; set; }

        public string province { get; set; }

        public string city { get; set; }

        public string area { get; set; }

        public string fullAddress { get; set; }

        public string longitude { get; set; }

        public string latitude { get; set; }

        public string electricFence { get; set; }

        public string shopPicture { get; set; }

        public string shopTel { get; set; }

        public int? status { get; set; }

        public string sendType { get; set; }

        public double? unitPrice { get; set; }

        public DateTime? createTime { get; set; }
    }
}
