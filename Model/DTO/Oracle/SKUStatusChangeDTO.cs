namespace Model.DTO.Oracle
{
    public class SKUStatusChangeDTO
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
        /// 状态(淘汰、录入属于下架)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 处理成功
        /// </summary>
        public bool isSuccess { get; set; }
    }
}
