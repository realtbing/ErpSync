using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Oracle
{
    [Table("APPLYORDERDETAIL")]
    public class ApplyOrderDetail
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        public long ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? ORDERID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string ORDERCODE { get; set; }

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
        [Column(TypeName = "nvarchar2")]
        [StringLength(36)]
        public string ORIGINALDETAILID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? GOODSID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string GOODSCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal PPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal SPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? STOCKQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(19, 3)")]
        public decimal? SPEC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(7, 3)")]
        public decimal? TAX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal PLANQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? AUDITQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? FACTQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? REASONID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(100)]
        public string REASONINFO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? AUDITREASONID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(100)]
        public string AUDITREASONINFO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? OFFSETQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? OFFSETREASONID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "varchar2")]
        [StringLength(100)]
        public string OFFSETREASONINFO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? HASOFFSET { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? OFFSETMETHOD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(12, 3)")]
        public decimal? OFFSETVALUE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? OFFSETID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "varchar2")]
        [StringLength(50)]
        public string OFFSETNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? OFFSETON { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string STOCKBATCHID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string STOCKBATCHCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? STOCKBATCHPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? PRICEPROTECT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? REFORDERTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "varchar2")]
        [StringLength(36)]
        public string REFORDERDETAILID { get; set; }
    }
}
