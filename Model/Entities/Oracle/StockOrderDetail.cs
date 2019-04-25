﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Oracle
{
    [Table("APPLYORDERDETAIL")]
    public class StockOrderDetail
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        [Column(TypeName = "nvarchar2")]
        [StringLength(36)]
        public string ID { get; set; }

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
        [StringLength(50)]
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
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? FACTAMT { get; set; }

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
        public int? SIGN { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? QTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? PRODUCEDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
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
        [Column(TypeName = "nvarchar2")]
        [StringLength(100)]
        public string SUPPLIERCODE { get; set; }

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
        public decimal? BATCHAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? MARKUPAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? ORIGINALGOODSID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(50)]
        public string ORIGINALGOODSCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? IMPACTQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "nvarchar2")]
        [StringLength(36)]
        public string ORIGINALDETAILID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? TAXDEDUCTION { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(7, 3)")]
        public decimal? STAX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? RULEID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? RULEAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? FACTAMTNORULE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? SPECIFIEDPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? TAREQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
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
        [Column(TypeName = "varchar2")]
        [StringLength(50)]
        public string REFORDERCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? IMPACTAMT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? SPECIFIEDAMT { get; set; }

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
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? DCOUTPRICE { get; set; }

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
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? REALPRICE { get; set; }

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
        [Column(TypeName = "decimal(24, 6)")]
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

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(24, 6)")]
        public decimal? TAXDEDUCTIONAMT { get; set; }
    }
}
