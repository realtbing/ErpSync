using System;

namespace Model.Entities.Oracle
{
    public class StockOrderDetail
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string ID { get; set; }

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
        public long? STORAGEID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string STORAGECODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ORDERTYPE { get; set; }

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
        public long? GOODSID { get; set; }

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
        public decimal? PLANPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? FACTPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PLANAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? FACTAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SPEC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? TAX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? SIGN { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? QTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? PRODUCEDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PRODUCEBATCH { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EXPIRYDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? SUPPLIERID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SUPPLIERCODE { get; set; }

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
        public decimal? BATCHAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? MARKUPAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? ORIGINALGOODSID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ORIGINALGOODSCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? IMPACTQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ORIGINALDETAILID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? TAXDEDUCTION { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? STAX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? RULEID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? RULEAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? FACTAMTNORULE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SPECIFIEDPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? TAREQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? FIRSTPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? BUSINESSTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? REFORDERID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REFORDERCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? IMPACTAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SPECIFIEDAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? TAREWEIGHT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SHIPPERTAREWEIGHT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? GROSSWEIGHT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? PURCHASETYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? DCOUTPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string STOCKBATCHID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string STOCKBATCHCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? PURCHASERID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PURCHASERNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ORIBATCHID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ORIBATCHCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REFBATCHID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REFBATCHCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? REALPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? REFORDERTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REFORDERDETAILID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? TURNOVERID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TURNOVERCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? TURNOVERQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? GROUPID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? TAXDEDUCTIONAMT { get; set; }
    }
}
