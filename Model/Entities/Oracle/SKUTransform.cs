using System;

namespace Model.Entities.Oracle
{
    public class SKUTransform
    {
        /// <summary>
        /// 序号
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long MOTHERSKUID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MOTHERSKUCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long CHILDSKUID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CHILDSKUCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
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
        public string MODIFYBYNAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? MODIFYON { get; set; }
    }
}