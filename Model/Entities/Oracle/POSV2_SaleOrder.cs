using System;

namespace Model.Entities.Oracle
{
    public class POSV2_SaleOrder
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? SEQNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SERIALNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? SALEORDERTYPE { get; set; }

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
        public int? STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SHIFT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SALERCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SALERNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? UPLOADSTATUS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? UPLOADON { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UPLOADMSG { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? GOODSAMT { get; set; }

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
        public decimal? ORDERDISCOUNTAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? TOTALAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PAYMENTAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? CHANGEAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? LOSSAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? VIPAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DISCOUNTAUTHORCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DISCOUNTAUTHORNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CUSTOMERCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CARDCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? TOTALPOINT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? SOURCETYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REFSTORECODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REFPOSCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REFORDERCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CREATEON { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CREATEBYID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? MODIFYON { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MODIFYBYID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MODIFYBYNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CREATEBYNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PRINTCUSTOMERBALANCE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PRINTCUSTOMERPOINT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CUSTOMERTYPECODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CUSTOMERTYPENAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PAYMENTPOINT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? GIVEPOINT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? MATHAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? OFFLINE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? MATHRATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? CUSTOMERLEVEL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CUSTOMERLEVELNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EMPLOYEECODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EMPLOYEENAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UPLOADIP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CREATEBYIP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CREATEBYMAC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ISCHORDER { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? CHTAXAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REGIONNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REGIONCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REGIONPATH { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DELIVERYNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DELIVERYADDRESS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DELIVERYPHONE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SERVICETYPECODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SERVICETYPENAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CUSTOMERPHONE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TABLECODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TABLENAME { get; set; }
    }
}
