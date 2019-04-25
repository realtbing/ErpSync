using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Oracle
{
    [Table("DELIVERYORDER")]
    public class DeliveryOrder
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
        public int? DELIVERYORDERTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? DELIVERYTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? SETTLEMENTTYPE { get; set; }

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
        public DateTime? EXPIRYDATE { get; set; }

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
        public long? GOODSCATEID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string GOODSCATECODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? GROUPLEVEL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? PURCHASEDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string DELIVERBYNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string DELIVERBYTEL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? RECEIVEDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal DELIVERYAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? RECEIVEAMT { get; set; }
    }
}
