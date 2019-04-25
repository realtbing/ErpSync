using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Oracle
{
    [Table("SKUTRANSFORM")]
    public class SKUTransform
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        public long ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long MOTHERSKUID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        [Required]
        public string MOTHERSKUCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long CHILDSKUID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        [Required]
        public string CHILDSKUCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? QTYPERMOTHER { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? SEQNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? CREATEBYID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string CREATEBYNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CREATEON { get; set; }

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
    }
}