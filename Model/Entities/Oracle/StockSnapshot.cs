using System;

namespace Model.Entities.Oracle
{
    public class StockSnapshot
    {
        /// <summary>
        /// 日结日期
        /// </summary>
        public DateTime BACKUPDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long ORGID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ORGCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long STORAGEID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string STORAGECODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long GOODSID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GOODSCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PASSAGEQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? QTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? AMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ENDINDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ENDSALEDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ENDINVENTORYDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? INQTYDAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? LOSSQTYDAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SALEQTYDAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? INQTY3DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SALEQTY3DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? LOSSQTY3DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? INQTY7DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SALEQTY7DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? LOSSQTY7DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? INQTY28DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SALEQTY28DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? LOSSQTY28DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DESCRIPTION { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? CREATEBYID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CREATEON { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CREATEBYNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? MODIFYBYID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MODIFYBYNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? MODIFYON { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? REALQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? LOCKQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PREQTY { get; set; }
    }
}
