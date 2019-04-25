using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Oracle
{
    [Table("STOCKORDER")]
    public class StockOrder
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        public long ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string CODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? ORGID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string ORGCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? STORAGEID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
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
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string REFORGCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? REFSTORAGEID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
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
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string REFORGCODE2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? REFORDERTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string REFORDERCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? PLANAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? FACTAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(200)]
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
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string CREATEBYNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? MODIFYBYID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
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
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
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
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
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
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
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
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string FINANCECODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? RJDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
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
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
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
        [Column(TypeName = "nvarchar2")]
        [StringLength(100)]
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
        [Column(TypeName = "nvarchar2")]
        [StringLength(100)]
        public string RELATIONORDERCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? PASTEDAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IMPACTTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "varchar2")]
        [StringLength(100)]
        public string MANUALORDERCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? TAXDEDUCTIONAMT { get; set; }
    }
}
