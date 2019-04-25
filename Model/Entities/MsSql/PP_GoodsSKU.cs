namespace Model.Entities.MsSql
{
    public class PP_GoodsSKU
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int skuId { get; set; }

        /// <summary>
        /// SKU码
        /// </summary>
        public string skuCode { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string goodsCode { get; set; }

        /// <summary>
        /// 属性集合(e.g.规格:规格值,多个使用","隔开)
        /// </summary>
        public string attrIds { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }
    }
}