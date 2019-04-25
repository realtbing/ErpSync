namespace Model.DTO.Oracle
{
    public class ApplyOrderDetailDTO
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
        public int? ORDERTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ORGCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GOODSCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal PLANQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? AUDITQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? FACTQTY { get; set; }
    }
}
