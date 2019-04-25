using System;

namespace Model.Entities.MsSql
{
    public class PP_GoodsStockAlerm
    {
        public int listId { get; set; }
        
        public int goodsId { get; set; }

        public string goodsCode { get; set; }

        public string goodsName { get; set; }

        public int shopId { get; set; }

        public string shopCode { get; set; }

        public string shopName { get; set; }

        public DateTime createTime { get; set; }
    }
}