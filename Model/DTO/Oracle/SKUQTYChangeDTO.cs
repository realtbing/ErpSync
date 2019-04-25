namespace Model.DTO.Oracle
{
    public class SKUQTYChangeDTO
    {
        /// <summary>
        /// 触发数据Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 同TriggerType,Type=0时表示为初始化的库存
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string SKUCode { get; set; }

        /// <summary>
        /// 门店编码
        /// </summary>
        public string ShopCode { get; set; }

        /// <summary>
        /// 数量 
        /// </summary>
        public decimal Qty { get; set; }

        /// <summary>
        /// 处理成功
        /// </summary>
        public bool isSuccess { get; set; }
    }
}
