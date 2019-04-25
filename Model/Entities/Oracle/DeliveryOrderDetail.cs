using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Oracle
{
    [Table("DELIVERYORDERDETAIL")]
    public class DeliveryOrderDetail
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
        [Column(TypeName = "nvarchar2")]
        [StringLength(36)]
        public string GOODSBARCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? PLANPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? FACTPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? PLANAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(19, 3)")]
        public decimal? PLANQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(7, 3)")]
        public decimal? FACTQTY { get; set; }

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
        [Column(TypeName = "nvarchar2")]
        [StringLength(100)]
        public string BOXCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? PRODUCEDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(100)]
        public string PRODUCEBATCH { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EXPIRYDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? GOODSCATEID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "varchar2")]
        [StringLength(100)]
        public string GOODSCATECODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? SEQNO { get; set; }

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
        public int? CONFIRMSTATUS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CONFIRMTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? CONFIRMPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? CONFIRMPAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? SYNCPPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? ORIPLANQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? PURCHASESTATUS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? PURCHASERID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string PURCHASERNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? TAREWEIGHT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? SHIPPERTAREWEIGHT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? GROSSWEIGHT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? PURCHASETYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(36)]
        public string ORIBATCHID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string ORIBATCHCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(36)]
        public string REFBATCHID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string REFBATCHCODE { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public long? TURNOVERID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "varchar2")]
        [StringLength(50)]
        public string TURNOVERCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? TURNOVERQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? GROUPID { get; set; }
    }
}
