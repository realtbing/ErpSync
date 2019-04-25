namespace Model.DTO.Oracle
{
    public class POSV2_SaleOrderDetailDTO
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string STORECODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string STORENAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string POSCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string POSNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ORDERID { get; set; }

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
        public decimal? ORIGINALTAREQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? ORIGINALQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? QTY { get; set; }
    }
}
