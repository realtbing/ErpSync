namespace Model.DTO.Oracle
{
    public class DeliveryOrderDetailDTO
    {
        /// <summary>
        /// 序号
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? ORDERID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ORDERCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? DELIVERYORDERTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? ORGID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ORGCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? REFORGID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REFORGCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? GOODSID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GOODSCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PLANQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? FACTQTY { get; set; }
    }
}
