namespace Model.DTO.Oracle
{
    public class SKUPriceChangeDTO
    {
        /// <summary>
        /// 触发数据Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string PluCode { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string SKUCode { get; set; }

        /// <summary>
        /// 门店编码
        /// </summary>
        public string ShopCode { get; set; }

        /// <summary>
        /// 最新价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 处理成功
        /// </summary>
        public bool isSuccess { get; set; }
    }
}
