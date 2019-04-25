using System;

namespace Model.Entities.MsSql
{
    public class PP_GoodsPriceAlerm
    {
        public int listId { get; set; }

        public int goodsId { get; set; }

        public string goodsPluCode { get; set; }

        public string goodsBarCode { get; set; }

        public string goodsCode { get; set; }

        public string goodsName { get; set; }

        public int shopId { get; set; }

        public string shopCode { get; set; }

        public string shopName { get; set; }

        /// <summary>
        /// 原价格
        /// </summary>
        public decimal originPrice { get; set; }

        /// <summary>
        /// 新价格(根据转换率，通过母码价格换算出子码价格)
        /// </summary>
        public decimal newPrice { get; set; }

        /// <summary>
        /// 确认的价格
        /// </summary>
        public decimal? confirmPrice { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }

        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? confirmTime { get; set; }
    }
}
