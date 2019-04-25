using System;

namespace Model.Entities.MsSql
{    
    public class PP_ShopGoods
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int sgid { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int shopId { get; set; }

        /// <summary>
        /// 库存Id
        /// </summary>
        public int skuId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        public double? goodsPrice { get; set; }

        /// <summary>
        /// 划线价
        /// </summary>
        public double? publicPrice { get; set; }

        /// <summary>
        /// 采购价
        /// </summary>
        public double? purchasePrice { get; set; }

        /// <summary>
        /// 兑换积分
        /// </summary>
        public int? integralCost { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public double? inventory { get; set; }

        /// <summary>
        /// 销售量
        /// </summary>
        public int? salesCnt { get; set; }

        /// <summary>
        /// 提成比例
        /// </summary>
        public double? commissionRate { get; set; }

        /// <summary>
        /// 限购类型
        /// </summary>
        public int? limitType { get; set; }

        /// <summary>
        /// 限购价格
        /// </summary>
        public double? limitPrice { get; set; }

        /// <summary>
        /// 限购数量
        /// </summary>
        public int? limitQty { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        public int? browseNo { get; set; }

        /// <summary>
        /// 收藏量
        /// </summary>
        public int? collectionNo { get; set; }

        /// <summary>
        /// 分享量
        /// </summary>
        public int? shareNo { get; set; }

        /// <summary>
        /// 状态(1:上架;2:下架)
        /// </summary>
        public int? status { get; set; }

        /// <summary>
        /// 是否读取线下库存
        /// </summary>
        public bool? isOfflineSKU { get; set; }

        /// <summary>
        /// 是否读取线下价格
        /// </summary>
        public bool? isOfflineSKUPrice { get; set; }

        /// <summary>
        /// 最大售后天数
        /// </summary>
        public int? daysAfterSale { get; set; }

        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime? shelveTime { get; set; }

        /// <summary>
        /// 下架时间
        /// </summary>
        public DateTime? offshelveTime { get; set; }
    }
}