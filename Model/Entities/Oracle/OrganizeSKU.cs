using System;

namespace Model.Entities.Oracle
{
    public class OrganizeSKU
    {
        /// <summary>
        /// 序号
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public long SKU_ID { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public long ORGANIZE_ID { get; set; }

        /// <summary>
        /// 经营状态(1:经营;2不经营;)
        /// </summary>
        public long? SALESTATUS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? ORDERSTATUS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? ORDERTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? RECEIPTDC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? SHIPDC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? DELIVERYTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? RETURNTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? INPUTVATRATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? OUTPUTVATRATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? SUPPLIER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SPRICE0 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? MPRICE0 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? MPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? DCRATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? INCRATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? LOSERATE { get; set; }

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
        public string CREATEBYNAME { get; set; }

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
        public decimal? DCPLACERATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? BUSINESSTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SKU_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ORGANIZE_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal DISPLAYQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ISCHGOODS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? CHTAX { get; set; }

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
        public decimal? MAXSALEDIFFQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? RECEIPTDIFFRATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ISSCALEWEIGHT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ISPUTON { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? MINSTOCK { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? MAXSTOCK { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PICKDIFFRATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SALEBONUSRATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? STOCKFIXDIFFRATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SALEDIFFRATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PURCHASEBONUSRATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PPRICE0 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? STOCKFIXDIFFRATE2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? MAXSALEDIFFQTY2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PICKDIFFRATE2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? RECEIPTDIFFRATE2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SALEDIFFPUNISH { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ORIGIN { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? STOCKDIFFPUNISH { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? STOCKDIFFPUNISH2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? RETURNWAY { get; set; }
    }
}