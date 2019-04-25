using System;

namespace Model.Entities.MsSql
{
    public class PP_Goods
    {
        public int goodsId { get; set; }

        public string goodsCode { get; set; }

        public string skuGoodsCode { get; set; }

        public string goodsName { get; set; }

        public string goodsTypeCode { get; set; }

        public string subTypeCode { get; set; }

        public string groupCode { get; set; }

        public string barCode { get; set; }

        public string goodsPicture { get; set; }

        public string thumbnail { get; set; }

        public string sharePicture { get; set; }

        public string goodsDetailInfo { get; set; }

        public int? sort { get; set; }

        public string weCharShare { get; set; }

        public DateTime? createTime { get; set; }

        public DateTime? editTime { get; set; }
    }
}
