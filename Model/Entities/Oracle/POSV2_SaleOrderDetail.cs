using System;

namespace Model.Entities.Oracle
{
    [Table("POSV2_SALEORDERDETAIL")]
    public class POSV2_SaleOrderDetail
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
        public int? DETAILSEQNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GOODSCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GOODSBARCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GOODSSCANBARCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GOODSNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UNIT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SPEC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? TAX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CURRENCY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string POINTUNIT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? SCALETYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CATECODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CATENAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? PRICETYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? NORMALPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? ORIGINALPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? POINT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PRICE { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public decimal? ORIGINALAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? GOODSAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PLANAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? TOTALAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? TOTALPOINT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SHAREAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PROMOTIONAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? DISCOUNTAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? LOSSAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PAYMENTAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? DETAILSTATUS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ISCHGOODS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? CHTAX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? CHTAXAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PROMOTIONCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PROMOTIONNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GROUPCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? GROUPQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? GIFTPOINTVALUE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ISGIFT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ORIGINALPRICETYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? PROMOTIONPRICETYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? UPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? UPOINT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? TOTALWEIGHT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? PLANPACKQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? FACTPACKQTY { get; set; }
    }
}
