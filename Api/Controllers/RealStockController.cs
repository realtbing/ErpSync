using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Logic.Oracle;
using Model;

namespace Api.Controllers
{
    public class RealStockController : ControllerBase
    {
        POSV2_SaleOrderDetailLogic posv2_SaleOrderDetailLogic = new POSV2_SaleOrderDetailLogic();
        SKUTransformLogic skuTransformLogic = new SKUTransformLogic();
        StockLogic stockLogic = new StockLogic();
        StockOrderDetailLogic stockOrderDetailLogic = new StockOrderDetailLogic();
        StockSnapshotLogic stockSnapshotLogic = new StockSnapshotLogic();

        /// <summary>
        /// 获取ERP系统里门店商品的库存(从日结日开始统计销售、报损、退货订单的商品总量)
        /// </summary>
        /// <param name="orgCode">门店Code</param>
        /// <param name="skuCode">商品Code</param>
        /// <returns>返回价格</returns>
        public ReturnModel<decimal> Get(string orgCode, string skuCode)
        {
            ReturnModel<decimal> result = new ReturnModel<decimal>();
            //检查当前请求的skucode是否为母码
            var skuTFList = skuTransformLogic.GetListByMotherCode(skuCode);
            if (skuTFList.Count > 0)
            {
                result.Type = Models.ResultType.Error;
                result.Errorcode = "100002";
                result.Message = "Current request code is mother code";
            }
            else
            {
                decimal qty = 0;

                //获取商品的库存信息
                var stockEntity = stockLogic.GetSingleByShopAndSKU(orgCode, skuCode);
                if (stockEntity == null)
                {
                    result.Type = Models.ResultType.Error;
                    result.Errorcode = "100004";
                    result.Message = "Current request code is not stock";
                }
                else
                {
                    decimal inventory = 0;
                    #region 获取自己的库存
                    //商品库存
                    var skuQty = stockEntity.QTY.Value;
                    //获取商品的最后日结信息
                    var stockSnapshotEntity = stockSnapshotLogic.GetLastStockSnapshot(orgCode, skuCode);
                    //日结日的库存
                    var skuSnapshotQty = stockSnapshotEntity == null ? 0 : stockSnapshotEntity.QTY.HasValue ? stockSnapshotEntity.QTY.Value : 0;
                    //日结日
                    var snapshotTime = stockSnapshotEntity == null ? new DateTime(1970, 1, 1) : stockSnapshotEntity.BACKUPDATE.AddDays(1);
                    //获取从日结开始到当前的收银订单统计
                    var posStatistics = posv2_SaleOrderDetailLogic.GetStatisticsByOrgAndSKU(orgCode, skuCode, snapshotTime, DateTime.Now.AddDays(1).Date.AddSeconds(-1));
                    //获取从日结开始到当前的库存订单统计(配送入库、调拨入库、送货入库、报损出库、调拨出库、退货出库)
                    var stockOrderStatistics = stockOrderDetailLogic.GetStatisticsByOrgAndSKU(orgCode, skuCode, snapshotTime, DateTime.Now.AddDays(1).Date.AddSeconds(-1));

                    if (stockSnapshotEntity != null)
                    {
                        //更新为真实库存(日结库存+收银退货+配送入库+调拨入库+送货入库-收银销售-报损出库-调拨出库-退货出库)
                        inventory = skuSnapshotQty + posStatistics[(int)POSV2_SaleOrderType.Cancel] + stockOrderStatistics[(int)StockOrderType.DistributeIn] + stockOrderStatistics[(int)StockOrderType.AllotIn] + stockOrderStatistics[(int)StockOrderType.Delivery]
                                                   - posStatistics[(int)POSV2_SaleOrderType.Sale] - stockOrderStatistics[(int)StockOrderType.ReportLoss] - stockOrderStatistics[(int)StockOrderType.AllotOut] - stockOrderStatistics[(int)StockOrderType.DeliveryCancel];
                    }
                    else
                    {
                        inventory = skuQty + posStatistics[(int)POSV2_SaleOrderType.Cancel] + stockOrderStatistics[(int)StockOrderType.DistributeIn] + stockOrderStatistics[(int)StockOrderType.AllotIn] + stockOrderStatistics[(int)StockOrderType.Delivery]
                                           - posStatistics[(int)POSV2_SaleOrderType.Sale] - stockOrderStatistics[(int)StockOrderType.ReportLoss] - stockOrderStatistics[(int)StockOrderType.AllotOut] - stockOrderStatistics[(int)StockOrderType.DeliveryCancel];
                    }
                    #endregion

                    //判断是否标品
                    var skuTFListByChild = skuTransformLogic.GetListByChildCode(skuCode);
                    if (skuTFListByChild.Count == 0)
                    {
                        qty = inventory;
                    }
                    else
                    {
                        #region 获取商品母码的库存
                        var skuTFEntityByChild = skuTFListByChild.FirstOrDefault();
                        if (!skuTFEntityByChild.QTYPERMOTHER.HasValue)
                        {
                            result.Type = Models.ResultType.Error;
                            result.Errorcode = "100003";
                            result.Message = "Not found sku transform";
                        }
                        else
                        {
                            //获取母码的库存
                            var stockEntityByMotherCode = stockLogic.GetSingleByShopAndSKU(orgCode, skuTFEntityByChild.MOTHERSKUCODE);
                            if (stockEntityByMotherCode == null)
                            {
                                result.Type = Models.ResultType.Error;
                                result.Errorcode = "100004";
                                result.Message = "Current request code is not stock";
                            }
                            else
                            {
                                //母码商品库存
                                var skuQtyByMotherCode = stockEntityByMotherCode.QTY.Value;
                                //获取母码的最后日结信息
                                var stockSnapshotEntityByMotherCode = stockSnapshotLogic.GetLastStockSnapshot(orgCode, skuTFEntityByChild.MOTHERSKUCODE);
                                //日结日的库存
                                var skuQtyyByMotherCode = stockSnapshotEntityByMotherCode == null ? 0 : stockSnapshotEntityByMotherCode.QTY.HasValue ? stockSnapshotEntityByMotherCode.QTY.Value : 0;
                                //日结日
                                var snapshotTimeByMotherCode = stockSnapshotEntityByMotherCode == null ? new DateTime(1970, 1, 1) : stockSnapshotEntityByMotherCode.BACKUPDATE.AddDays(1);
                                //获取母码从日结开始到当前的收银订单统计
                                var posStatisticsByMotherCode = posv2_SaleOrderDetailLogic.GetStatisticsByOrgAndSKU(orgCode, skuTFEntityByChild.MOTHERSKUCODE, snapshotTimeByMotherCode, DateTime.Now.AddDays(1).Date.AddSeconds(-1));
                                //获取母码从日结开始到当前的库存订单统计(配送入库、调拨入库、送货入库、报损出库、调拨出库、退货出库)
                                var stockOrderStatisticsByMotherCode = stockOrderDetailLogic.GetStatisticsByOrgAndSKU(orgCode, skuTFEntityByChild.MOTHERSKUCODE, snapshotTimeByMotherCode, DateTime.Now.AddDays(1).Date.AddSeconds(-1));

                                if (stockSnapshotEntityByMotherCode != null)
                                {
                                    //更新为真实库存(日结库存+收银退货+配送入库+调拨入库+送货入库-收银销售-报损出库-调拨出库-退货出库)
                                    inventory = inventory + skuSnapshotQty + posStatisticsByMotherCode[(int)POSV2_SaleOrderType.Cancel] + stockOrderStatisticsByMotherCode[(int)StockOrderType.DistributeIn] + stockOrderStatisticsByMotherCode[(int)StockOrderType.AllotIn] + stockOrderStatisticsByMotherCode[(int)StockOrderType.Delivery]
                                                                           - posStatisticsByMotherCode[(int)POSV2_SaleOrderType.Sale] - stockOrderStatisticsByMotherCode[(int)StockOrderType.ReportLoss] - stockOrderStatisticsByMotherCode[(int)StockOrderType.AllotOut] - stockOrderStatisticsByMotherCode[(int)StockOrderType.DeliveryCancel];
                                }
                                else
                                {
                                    inventory = inventory + skuQty + posStatisticsByMotherCode[(int)POSV2_SaleOrderType.Cancel] + stockOrderStatisticsByMotherCode[(int)StockOrderType.DistributeIn] + stockOrderStatisticsByMotherCode[(int)StockOrderType.AllotIn] + stockOrderStatisticsByMotherCode[(int)StockOrderType.Delivery]
                                                                   - posStatisticsByMotherCode[(int)POSV2_SaleOrderType.Sale] - stockOrderStatisticsByMotherCode[(int)StockOrderType.ReportLoss] - stockOrderStatisticsByMotherCode[(int)StockOrderType.AllotOut] - stockOrderStatisticsByMotherCode[(int)StockOrderType.DeliveryCancel];
                                }
                            }

                            //根据母码库存通过转换率计算子码的库存
                            qty = Math.Floor(inventory / skuTFEntityByChild.QTYPERMOTHER.Value);
                        }
                        #endregion
                    }
                    result.Result = qty;
                }
            }
            
            return result;
        }
    }
}
