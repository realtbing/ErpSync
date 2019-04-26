using System;

namespace Model.Entities.Oracle
{
    public class StockOrder
    {
        /// <summary>
        /// 序号
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CODE { get; set; }

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
        public int? REFORGTYPE { get; set; }

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
        public long? REFSTORAGEID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REFSTORAGECODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? REFORGTYPE2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? REFORGID2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REFORGCODE2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? REFORDERTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string REFORDERCODE { get; set; }

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
        public long? AUDITBYID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? AUDITON { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AUDITBYNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? APPROVEBYID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? APPROVEON { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string APPROVEBYNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? PRINTCOUNT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ISIMPACT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VOUCHINGUSER { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? VOUCHINGDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? FINANCEID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FINANCECODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? RJDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CHECKBILLUSER { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CHECKBILLDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? PURCHASEBYID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PURCHASEBYNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? PURCHASEDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? STATEMENTID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string STATEMENTCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? REALAUDITON { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? GETORDERTYPEID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ISLOSSBILL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EFFECTDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? RELATIONORDERID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RELATIONORDERCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PASTEDAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IMPACTTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MANUALORDERCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? TAXDEDUCTIONAMT { get; set; }
    }
}
