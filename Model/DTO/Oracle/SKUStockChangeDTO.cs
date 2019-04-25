namespace Model.DTO.Oracle
{
    public class SKUStockChangeDTO
    {
        /// <summary>
        /// 触发数据Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string SKUCode { get; set; }

        /// <summary>
        /// 门店编码
        /// </summary>
        public string ShopCode { get; set; }

        /// <summary>
        /// 最新库存 
        /// </summary>
        public decimal Qty { get; set; }

        /// <summary>
        /// 是否标品
        /// </summary>
        public bool isStandard { get; set; }

        /// <summary>
        /// 处理成功
        /// </summary>
        public bool isSuccess { get; set; }
    }
}
