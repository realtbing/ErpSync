namespace Model.DTO.Oracle
{
    public class SKUQTYPERChangeDTO
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
        ///  变更前
        /// </summary>
        public decimal QtyPerBefore { get; set; }

        /// <summary>
        ///  变更后
        /// </summary>
        public decimal QtyPerAfter { get; set; }

        /// <summary>
        /// 处理成功
        /// </summary>
        public bool isSuccess { get; set; }
    }
}
