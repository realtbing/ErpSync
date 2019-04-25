using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Oracle
{
    [Table("STOCKSNAPSHOT")]
    public class StockSnapshot
    {
        /// <summary>
        /// 日结日期
        /// </summary>
        [Key, Column(Order = 0)]
        public DateTime BACKUPDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key, Column(Order = 1)]
        [Required]
        public long ORGID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key, Column(Order = 2, TypeName = "nvarchar2")]
        [StringLength(50)]
        [Required]
        public string ORGCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key, Column(Order = 3)]
        [Required]
        public long STORAGEID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key, Column(Order = 4, TypeName = "nvarchar2")]
        [StringLength(50)]
        public string STORAGECODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key, Column(Order = 5)]
        [Required]
        public long GOODSID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key, Column(Order = 6, TypeName = "nvarchar2")]
        [StringLength(50)]
        [Required]
        public string GOODSCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? PRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? PASSAGEQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? QTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
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
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? INQTYDAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? LOSSQTYDAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? SALEQTYDAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? INQTY3DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? SALEQTY3DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? LOSSQTY3DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? INQTY7DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? SALEQTY7DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? LOSSQTY7DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? INQTY28DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? SALEQTY28DAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? LOSSQTY28DAY { get; set; }

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
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? REALQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? LOCKQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? PREQTY { get; set; }
    }
}
