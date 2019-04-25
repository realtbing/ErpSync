using System;

namespace Model.Entities.Oracle
{
    public class SKU
    {
        /// <summary>
        /// 序号
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? CATEGORY_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SHORTNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WBCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PYCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? BRAND_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MODEL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? RANK { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? PRICETYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long BILLTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? MANAGETYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal INPUTVATRATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal OUTPUTVATRATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SPEC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UNIT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SALESPEC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SALEUNIT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? GROUPID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? SUPPLIER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IMGURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? PPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? MPRICE { get; set; }

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
        public string CATEGORY_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long CATEGORY_ID1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CATEGORY_CODE1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long CATEGORY_ID2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CATEGORY_CODE2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long CATEGORY_ID3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CATEGORY_CODE3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long CATEGORY_ID4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CATEGORY_CODE4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? VALIDDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PLUCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BARCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GOODSSPEC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal DISPLAYQTY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? ORDERSPEC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string STORAGECONDITION { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NICKNAME { get; set; }

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
        public decimal? ALLOWADJUSTSPRICE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ISPASSCHECK { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ISLARGESIZE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CREATEBYORGCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CREATEBYORGNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ISPOSCREATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ORDERUNIT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CUSTOMERCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ISPARTSPICKING { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? CGCYL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? FJCYL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? TAREWEIGHT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ISTRACKEDBACK { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? BOXTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? MANAGEMODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? VOUCHERCATELEVEL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TAXCODE { get; set; }
    }
}
