using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Logic.MsSql;
using Logic.Oracle;
using Model;
using Model.DTO.Oracle;
using Model.Entities.Oracle;
using Model.Profiles.Oracle;

namespace Logic
{
    public class Process
    {
        public Process()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<OrganizeSKUProfile>();
                x.AddProfile<SKUProfile>();
                x.AddProfile<StockProfile>();
            });
        }

        public void UpdateGoodsPriceInFull()
        {
            try
            {
                PP_ShopLogic shopLogic = new PP_ShopLogic();
                PP_ShopGoodsLogic shopGoodsLogic = new PP_ShopGoodsLogic();
                OrganizeSKULogic organizeSKULogic = new OrganizeSKULogic();

                //根据线上门店获取
                var shopList = shopLogic.GetList(1);
                foreach (var shop in shopList)
                {
                    int count = 0;
                    int totalCount = 0;
                    int pageIndex = 0;
                    int pageSize = 100;
                    while (count <= totalCount)
                    {
                        var skuDTOList = organizeSKULogic.GetPagerList(shop.shopCode, "", out totalCount, pageIndex, pageSize);
                        pageIndex++;
                        count = (pageIndex) * pageSize;
                        var result = shopGoodsLogic.UpdateGoodsPrice(skuDTOList);
                        if (!result)
                        {
                            break;
                        }
                    }
                    Console.WriteLine("Shop:" + shop.shopCode + ", goods price update success");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.ToString());
            }
        }

        public void UpdateGoodsInventoryInFull()
        {
            try
            {
                PP_ShopLogic shopLogic = new PP_ShopLogic();
                PP_ShopGoodsLogic shopGoodsLogic = new PP_ShopGoodsLogic();
                
                POSV2_SaleOrderDetailLogic posv2_SaleOrderDetailLogic = new POSV2_SaleOrderDetailLogic();
                SKUTransformLogic skuTransformLogic = new SKUTransformLogic();
                StockLogic stockLogic = new StockLogic();
                StockOrderDetailLogic stockOrderDetailLogic = new StockOrderDetailLogic();
                StockSnapshotLogic stockSnapshotLogic = new StockSnapshotLogic();

                var shopList = shopLogic.GetList(1);
                foreach (var shop in shopList)
                {
                    Console.WriteLine("shop: " + shop.shopCode + ", start statistics real inventory");
                    List<SKUQTYChangeDTO> stockDTOList = new List<SKUQTYChangeDTO>();

                    //第一步: 获取所有商品的库存信息
                    List<Stock> stockList = stockLogic.GetList(shop.shopCode);
                    Console.WriteLine("shop:" + shop.shopCode + ", goods count:" + stockList.Count);

                    //第二步: 获取最后日结信息
                    var stockSnapshotList = stockSnapshotLogic.GetLastStockSnapshot(shop.shopCode);
                    Console.WriteLine("shop:" + shop.shopCode + ", snapshot goods count: " + stockSnapshotList.Count);
                    //日结的商品信息
                    var goodsList = stockSnapshotList.Select(x => x.GOODSCODE);

                    //第三步: 根据日结的商品信息更新为真实库存
                    foreach (var stock in stockList)
                    {
                        //获取商品的日结库存
                        var ssModel = stockSnapshotList.Where(x => x.GOODSCODE.Equals(stock.GOODSCODE)).FirstOrDefault();
                        var skuSnapshotQty = ssModel == null ? 0 : ssModel.QTY.HasValue ? ssModel.QTY.Value : 0;
                        var skuQty = stock.QTY.HasValue ? stock.QTY.Value : 0;
                        if (skuSnapshotQty != skuQty)
                        {
                            Console.WriteLine("shop:" + shop.shopCode + ", goods:" + stock.GOODSCODE + ", current inventory and stocksnapshop inventory are inconsistent");
                        }
                        //日结日
                        var skuSnapshotTime = ssModel == null ? new DateTime(1970, 1, 1) : ssModel.BACKUPDATE.Date.AddDays(1);

                        //获取从日结开始到当前的收银订单统计(销售、退货)
                        var posStatistics = posv2_SaleOrderDetailLogic.GetStatisticsByOrgAndSKU(shop.shopCode, stock.GOODSCODE, skuSnapshotTime, DateTime.Now.AddDays(1).Date.AddSeconds(-1));

                        //获取从日结开始到当前的库存订单统计(配送入库、调拨入库、送货入库、报损出库、调拨出库、退货出库)
                        var stockOrderStatistics = stockOrderDetailLogic.GetStatisticsByOrgAndSKU(shop.shopCode, stock.GOODSCODE, skuSnapshotTime, DateTime.Now.AddDays(1).Date.AddSeconds(-1));

                        if (goodsList.Contains(stock.GOODSCODE))
                        {
                            //更新为真实库存(日结库存+收银退货+配送入库+调拨入库+送货入库-收银销售-报损出库-调拨出库-退货出库)
                            stock.QTY = skuSnapshotQty + posStatistics[(int)POSV2_SaleOrderType.Cancel] + stockOrderStatistics[(int)StockOrderType.DistributeIn] + stockOrderStatistics[(int)StockOrderType.AllotIn] + stockOrderStatistics[(int)StockOrderType.Delivery]
                                                       - posStatistics[(int)POSV2_SaleOrderType.Sale] - stockOrderStatistics[(int)StockOrderType.ReportLoss] - stockOrderStatistics[(int)StockOrderType.AllotOut] - stockOrderStatistics[(int)StockOrderType.DeliveryCancel];
                        }
                        else
                        {
                            //没有日结的商品，从收银订单中更新真实库存
                            stock.QTY = skuQty + posStatistics[(int)POSV2_SaleOrderType.Cancel]
                                               - posStatistics[(int)POSV2_SaleOrderType.Sale];
                        }
                    }

                    //第四步: 获取所有非标品
                    var notStandardSKUList = skuTransformLogic.GetList(stockList.Select(x => x.GOODSCODE).ToList());
                    var notStandardSKUListByMotherCode = notStandardSKUList.Select(x => x.MOTHERSKUCODE);
                    var notStandardSKUListByChildCode = notStandardSKUList.Select(x => x.CHILDSKUCODE);

                    //第五步: 从母码全部转换为子码库存（标品看作是子码， 非标品根据转换率换算）
                    var standardStockList = stockList.Where(x => !notStandardSKUListByMotherCode.Contains(x.GOODSCODE)).Where(x => !notStandardSKUListByChildCode.Contains(x.GOODSCODE)).ToList();
                    var notStandardStockList = stockList.Except(standardStockList, new StockListEquality()).ToList();

                    //非标品里的母码(有库存)
                    var notStandardMotherCodeStockList = notStandardStockList.Where(x => notStandardSKUListByMotherCode.Contains(x.GOODSCODE) && x.QTY > 0).ToList();

                    //从非标品里的母码找出关联的所有子码，并根据转换率计算子码的库存，如果子码含有库存，则加上
                    foreach (var motherCodeStock in notStandardMotherCodeStockList)
                    {
                        var childList = skuTransformLogic.GetChildList(motherCodeStock.GOODSCODE);
                        foreach (var child in childList)
                        {
                            var childStockModel = notStandardStockList.Where(x => x.ORGCODE.Equals(motherCodeStock.ORGCODE) && x.GOODSCODE.Equals(child.CHILDSKUCODE)).FirstOrDefault();
                            if (childStockModel != null)
                            {
                                Stock item = new Stock();
                                item.ORGCODE = motherCodeStock.ORGCODE;
                                item.GOODSCODE = child.CHILDSKUCODE;
                                item.QTY = Math.Floor((motherCodeStock.QTY.Value + (childStockModel.QTY.HasValue ? childStockModel.QTY.Value : 0)) / child.QTYPERMOTHER.Value);
                                standardStockList.Add(item);
                            }
                        }
                    }

                    //转换
                    stockDTOList = Mapper.Map<List<SKUQTYChangeDTO>>(standardStockList);
                    var result = shopGoodsLogic.UpdateGoodsInventory(stockDTOList);
                    if (!result)
                    {
                        break;
                    }
                    Console.WriteLine("shop: " + shop.shopCode + " inventory update succeses");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.ToString());
            }
        }

        public void UpdateGoodsIncrement()
        {
            try
            {
                PP_GoodsPriceAlermLogic goodsPriceAlermLogic = new PP_GoodsPriceAlermLogic();
                PP_ShopLogic shopLogic = new PP_ShopLogic();
                PP_ShopGoodsLogic shopGoodsLogic = new PP_ShopGoodsLogic();

                OrganizeSKULogic organizeSKULogic = new OrganizeSKULogic();
                POSV2_SaleOrderDetailLogic posv2_SaleOrderDetailLogic = new POSV2_SaleOrderDetailLogic();
                PP_TriggerDataBakLoigc pp_TriggerDataLogic = new PP_TriggerDataBakLoigc();
                SKULogic skuLogic = new SKULogic();
                SKUTransformLogic skuTransformLogic = new SKUTransformLogic();
                StockLogic stockLogic = new StockLogic();
                StockOrderDetailLogic stockOrderDetailLogic = new StockOrderDetailLogic();
                StockSnapshotLogic stockSnapshotLogic = new StockSnapshotLogic();

                while (true)
                {
                    var childSKUPriceList = new List<SKUPriceChangeDTO>();
                    var motherSKUPriceList = new List<SKUPriceChangeDTO>();
                    var qtyList = new List<SKUQTYChangeDTO>();
                    var statusList = new List<SKUStatusChangeDTO>();
                    var perList = new List<SKUQTYPERChangeDTO>();
                    var tfList = new List<SKUQTYPERChangeDTO>();

                    var list = pp_TriggerDataLogic.GetList(false, false);
                    foreach (var item in list)
                    {
                        switch (item.Type)
                        {
                            #region 商品价格更新
                            case (int)TriggerType.SKUChildPrice:
                                long orgSkuIdByChild = 0;
                                if (long.TryParse(item.DataId, out orgSkuIdByChild))
                                {
                                    var orgSkuEntity = organizeSKULogic.GetSingleById(orgSkuIdByChild);
                                    if (orgSkuEntity != null &&
                                        !string.IsNullOrEmpty(orgSkuEntity.ORGANIZE_CODE) &&
                                        !string.IsNullOrEmpty(orgSkuEntity.SKU_CODE) &&
                                        orgSkuEntity.SPRICE.HasValue)
                                    {
                                        OrganizeSKUBAKDTO skuDto = new OrganizeSKUBAKDTO();
                                        skuDto.triggerData = item;
                                        skuDto.sku = orgSkuEntity;

                                        childSKUPriceList.Add(Mapper.Map<SKUPriceChangeDTO>(skuDto));
                                    }
                                    else
                                    {
                                        pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                    }
                                }
                                else
                                {
                                    pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                }
                                break;
                            #endregion
                            #region 母码商品价格更新
                            case (int)TriggerType.SKUMotherPrice:
                                long orgSkuIdByMother = 0;
                                if (long.TryParse(item.DataId, out orgSkuIdByMother))
                                {
                                    var orgSkuEntity = organizeSKULogic.GetSingleById(orgSkuIdByMother);
                                    if (orgSkuEntity != null &&
                                        !string.IsNullOrEmpty(orgSkuEntity.ORGANIZE_CODE) &&
                                        !string.IsNullOrEmpty(orgSkuEntity.SKU_CODE) &&
                                        orgSkuEntity.SPRICE.HasValue)
                                    {
                                        //获取当前母码下所有的子码
                                        var skuTFListByMotherCode = skuTransformLogic.GetListByMotherCode(orgSkuEntity.SKU_CODE);
                                        foreach (var skuTF in skuTFListByMotherCode)
                                        {
                                            //获取子码的信息
                                            var skuModel = skuLogic.GetSingleByCode(orgSkuEntity.SKU_CODE);
                                            if (skuModel != null &&
                                                string.IsNullOrEmpty(skuModel.CODE) &&
                                                string.IsNullOrEmpty(skuModel.BARCODE) &&
                                                string.IsNullOrEmpty(skuModel.PLUCODE) &&
                                                skuTF.QTYPERMOTHER.HasValue)
                                            {
                                                SKUPriceChangeDTO skuPriceChangeDTO = new SKUPriceChangeDTO();
                                                skuPriceChangeDTO.Id = item.Id;
                                                skuPriceChangeDTO.BarCode = skuModel.BARCODE;
                                                skuPriceChangeDTO.PluCode = skuModel.PLUCODE;
                                                skuPriceChangeDTO.ShopCode = orgSkuEntity.ORGANIZE_CODE;
                                                skuPriceChangeDTO.SKUCode = skuTF.CHILDSKUCODE;
                                                skuPriceChangeDTO.Price = Math.Floor(orgSkuEntity.SPRICE.Value / skuTF.QTYPERMOTHER.Value);
                                                skuPriceChangeDTO.isSuccess = false;
                                                motherSKUPriceList.Add(skuPriceChangeDTO);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                    }
                                }
                                else
                                {
                                    pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                }
                                break;
                            #endregion
                            #region 商品状态变更
                            case (int)TriggerType.SKUStatus:
                                long skuId = 0;
                                if (long.TryParse(item.DataId, out skuId))
                                {
                                    var skuEntity = skuLogic.GetSingleById(skuId);
                                    if (skuEntity != null && !string.IsNullOrEmpty(skuEntity.CODE))
                                    {
                                        SKUBAKDTO skuDto = new SKUBAKDTO();
                                        skuDto.triggerData = item;
                                        skuDto.sku = skuEntity;

                                        statusList.Add(Mapper.Map<SKUStatusChangeDTO>(skuDto));
                                    }
                                    else
                                    {
                                        pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                    }
                                }
                                else
                                {
                                    pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                }
                                break;
                            #endregion
                            #region 配送入库库存更新
                            case (int)TriggerType.DistributeInStock:
                                var stockOrderDetailEntityBy16 = stockOrderDetailLogic.GetSingleById(item.DataId);
                                if (stockOrderDetailEntityBy16 != null &&
                                    !string.IsNullOrEmpty(stockOrderDetailEntityBy16.ORGCODE) &&
                                    !string.IsNullOrEmpty(stockOrderDetailEntityBy16.GOODSCODE) &&
                                    stockOrderDetailEntityBy16.QTY.HasValue)
                                {
                                    //检查是否为标品销售
                                    var stListByMother = skuTransformLogic.GetListByMotherCode(stockOrderDetailEntityBy16.GOODSCODE);
                                    var stListByChild = skuTransformLogic.GetListByChildCode(stockOrderDetailEntityBy16.GOODSCODE);
                                    if (stListByMother.Count == 0 && stListByChild.Count == 0)
                                    {
                                        var qtyDTO = new SKUQTYChangeDTO();
                                        qtyDTO.Id = item.Id;
                                        qtyDTO.Type = (int)TriggerType.DistributeInStock;
                                        qtyDTO.ShopCode = stockOrderDetailEntityBy16.ORGCODE;
                                        qtyDTO.SKUCode = stockOrderDetailEntityBy16.GOODSCODE;
                                        qtyDTO.Qty = stockOrderDetailEntityBy16.QTY.Value;
                                        qtyDTO.isSuccess = false;
                                        qtyList.Add(qtyDTO);
                                    }
                                    else
                                    {
                                        foreach (var st in stListByMother)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.DistributeInStock;
                                            qtyDTO.ShopCode = stockOrderDetailEntityBy16.ORGCODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(stockOrderDetailEntityBy16.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                        foreach (var st in stListByChild)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.DistributeInStock;
                                            qtyDTO.ShopCode = stockOrderDetailEntityBy16.ORGCODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(stockOrderDetailEntityBy16.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                    }
                                }
                                else
                                {
                                    pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                }
                                break;
                            #endregion
                            #region 调拨入库库存更新
                            case (int)TriggerType.AllotInStock:
                                var stockOrderDetailEntityBy5 = stockOrderDetailLogic.GetSingleById(item.DataId);
                                if (stockOrderDetailEntityBy5 != null &&
                                    !string.IsNullOrEmpty(stockOrderDetailEntityBy5.ORGCODE) &&
                                    !string.IsNullOrEmpty(stockOrderDetailEntityBy5.GOODSCODE) &&
                                    stockOrderDetailEntityBy5.QTY.HasValue)
                                {
                                    //检查是否为标品销售
                                    var stListByMother = skuTransformLogic.GetListByMotherCode(stockOrderDetailEntityBy5.GOODSCODE);
                                    var stListByChild = skuTransformLogic.GetListByChildCode(stockOrderDetailEntityBy5.GOODSCODE);
                                    if (stListByMother.Count == 0 && stListByChild.Count == 0)
                                    {
                                        var qtyDTO = new SKUQTYChangeDTO();
                                        qtyDTO.Id = item.Id;
                                        qtyDTO.Type = (int)TriggerType.AllotInStock;
                                        qtyDTO.ShopCode = stockOrderDetailEntityBy5.ORGCODE;
                                        qtyDTO.SKUCode = stockOrderDetailEntityBy5.GOODSCODE;
                                        qtyDTO.Qty = stockOrderDetailEntityBy5.QTY.Value;
                                        qtyDTO.isSuccess = false;
                                        qtyList.Add(qtyDTO);
                                    }
                                    else
                                    {
                                        foreach (var st in stListByMother)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.AllotInStock;
                                            qtyDTO.ShopCode = stockOrderDetailEntityBy5.ORGCODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(stockOrderDetailEntityBy5.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                        foreach (var st in stListByChild)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.AllotInStock;
                                            qtyDTO.ShopCode = stockOrderDetailEntityBy5.ORGCODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(stockOrderDetailEntityBy5.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                    }
                                }
                                else
                                {
                                    pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                }
                                break;
                            #endregion
                            #region 送货入库库存更新
                            case (int)TriggerType.DeliveryInStock:
                                var stockOrderDetailEntityBy3 = stockOrderDetailLogic.GetSingleById(item.DataId);
                                if (stockOrderDetailEntityBy3 != null &&
                                    !string.IsNullOrEmpty(stockOrderDetailEntityBy3.ORGCODE) &&
                                    !string.IsNullOrEmpty(stockOrderDetailEntityBy3.GOODSCODE) &&
                                    stockOrderDetailEntityBy3.QTY.HasValue)
                                {
                                    //检查是否为标品销售
                                    var stListByMother = skuTransformLogic.GetListByMotherCode(stockOrderDetailEntityBy3.GOODSCODE);
                                    var stListByChild = skuTransformLogic.GetListByChildCode(stockOrderDetailEntityBy3.GOODSCODE);
                                    if (stListByMother.Count == 0 && stListByChild.Count == 0)
                                    {
                                        var qtyDTO = new SKUQTYChangeDTO();
                                        qtyDTO.Id = item.Id;
                                        qtyDTO.Type = (int)TriggerType.DeliveryInStock;
                                        qtyDTO.ShopCode = stockOrderDetailEntityBy3.ORGCODE;
                                        qtyDTO.SKUCode = stockOrderDetailEntityBy3.GOODSCODE;
                                        qtyDTO.Qty = stockOrderDetailEntityBy3.QTY.Value;
                                        qtyDTO.isSuccess = false;
                                        qtyList.Add(qtyDTO);
                                    }
                                    else
                                    {
                                        foreach (var st in stListByMother)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.DeliveryInStock;
                                            qtyDTO.ShopCode = stockOrderDetailEntityBy3.ORGCODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(stockOrderDetailEntityBy3.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                        foreach (var st in stListByChild)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.DeliveryInStock;
                                            qtyDTO.ShopCode = stockOrderDetailEntityBy3.ORGCODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(stockOrderDetailEntityBy3.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                    }
                                }
                                else
                                {
                                    pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                }
                                break;
                            #endregion
                            #region 收银台退货入库库存更新
                            case (int)TriggerType.POSCancelInStock:
                                var posOrderDetailByCancelEntity = posv2_SaleOrderDetailLogic.GetSingleById(item.DataId);
                                if (posOrderDetailByCancelEntity != null &&
                                    !string.IsNullOrEmpty(posOrderDetailByCancelEntity.STORECODE) &&
                                    !string.IsNullOrEmpty(posOrderDetailByCancelEntity.GOODSCODE) &&
                                    posOrderDetailByCancelEntity.QTY.HasValue)
                                {
                                    //检查是否为标品销售
                                    var stListByMother = skuTransformLogic.GetListByMotherCode(posOrderDetailByCancelEntity.GOODSCODE);
                                    var stListByChild = skuTransformLogic.GetListByChildCode(posOrderDetailByCancelEntity.GOODSCODE);
                                    if (stListByMother.Count == 0 && stListByChild.Count == 0)
                                    {
                                        var qtyDTO = new SKUQTYChangeDTO();
                                        qtyDTO.Id = item.Id;
                                        qtyDTO.Type = (int)TriggerType.POSCancelInStock;
                                        qtyDTO.ShopCode = posOrderDetailByCancelEntity.STORECODE;
                                        qtyDTO.SKUCode = posOrderDetailByCancelEntity.GOODSCODE;
                                        qtyDTO.Qty = posOrderDetailByCancelEntity.QTY.Value;
                                        qtyDTO.isSuccess = false;
                                        qtyList.Add(qtyDTO);
                                    }
                                    else
                                    {
                                        foreach (var st in stListByMother)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.POSCancelInStock;
                                            qtyDTO.ShopCode = posOrderDetailByCancelEntity.STORECODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(posOrderDetailByCancelEntity.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                        foreach (var st in stListByChild)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.POSCancelInStock;
                                            qtyDTO.ShopCode = posOrderDetailByCancelEntity.STORECODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(posOrderDetailByCancelEntity.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                    }
                                }
                                else
                                {
                                    pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                }
                                break;
                            #endregion
                            #region 收银台销售出库库存更新
                            case (int)TriggerType.POSSaleOutStock:
                                var posOrderDetailBySaleEntity = posv2_SaleOrderDetailLogic.GetSingleById(item.DataId);
                                if (posOrderDetailBySaleEntity != null &&
                                    !string.IsNullOrEmpty(posOrderDetailBySaleEntity.STORECODE) &&
                                    !string.IsNullOrEmpty(posOrderDetailBySaleEntity.GOODSCODE) &&
                                    posOrderDetailBySaleEntity.QTY.HasValue)
                                {
                                    //检查是否为标品销售
                                    var stListByMother = skuTransformLogic.GetListByMotherCode(posOrderDetailBySaleEntity.GOODSCODE);
                                    var stListByChild = skuTransformLogic.GetListByChildCode(posOrderDetailBySaleEntity.GOODSCODE);
                                    if (stListByMother.Count == 0 && stListByChild.Count == 0)
                                    {
                                        var qtyDTO = new SKUQTYChangeDTO();
                                        qtyDTO.Id = item.Id;
                                        qtyDTO.Type = (int)TriggerType.POSSaleOutStock;
                                        qtyDTO.ShopCode = posOrderDetailBySaleEntity.STORECODE;
                                        qtyDTO.SKUCode = posOrderDetailBySaleEntity.GOODSCODE;
                                        qtyDTO.Qty = posOrderDetailBySaleEntity.QTY.Value;
                                        qtyDTO.isSuccess = false;
                                        qtyList.Add(qtyDTO);
                                    }
                                    else
                                    {
                                        foreach (var st in stListByMother)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.POSSaleOutStock;
                                            qtyDTO.ShopCode = posOrderDetailBySaleEntity.STORECODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(posOrderDetailBySaleEntity.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                        foreach (var st in stListByChild)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.POSSaleOutStock;
                                            qtyDTO.ShopCode = posOrderDetailBySaleEntity.STORECODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(posOrderDetailBySaleEntity.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                    }
                                }
                                else
                                {
                                    pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                }
                                break;
                            #endregion
                            #region 报损出库库存更新
                            case (int)TriggerType.LossOutStock:
                                var stockOrderDetailEntityBy13 = stockOrderDetailLogic.GetSingleById(item.DataId);
                                if (stockOrderDetailEntityBy13 != null &&
                                    !string.IsNullOrEmpty(stockOrderDetailEntityBy13.ORGCODE) &&
                                    !string.IsNullOrEmpty(stockOrderDetailEntityBy13.GOODSCODE) &&
                                    stockOrderDetailEntityBy13.QTY.HasValue)
                                {
                                    //检查是否为标品销售
                                    var stListByMother = skuTransformLogic.GetListByMotherCode(stockOrderDetailEntityBy13.GOODSCODE);
                                    var stListByChild = skuTransformLogic.GetListByChildCode(stockOrderDetailEntityBy13.GOODSCODE);
                                    if (stListByMother.Count == 0 && stListByChild.Count == 0)
                                    {
                                        var qtyDTO = new SKUQTYChangeDTO();
                                        qtyDTO.Id = item.Id;
                                        qtyDTO.Type = (int)TriggerType.LossOutStock;
                                        qtyDTO.ShopCode = stockOrderDetailEntityBy13.ORGCODE;
                                        qtyDTO.SKUCode = stockOrderDetailEntityBy13.GOODSCODE;
                                        qtyDTO.Qty = stockOrderDetailEntityBy13.QTY.Value;
                                        qtyDTO.isSuccess = false;
                                        qtyList.Add(qtyDTO);
                                    }
                                    else
                                    {
                                        foreach (var st in stListByMother)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.LossOutStock;
                                            qtyDTO.ShopCode = stockOrderDetailEntityBy13.ORGCODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(stockOrderDetailEntityBy13.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                        foreach (var st in stListByChild)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.LossOutStock;
                                            qtyDTO.ShopCode = stockOrderDetailEntityBy13.ORGCODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(stockOrderDetailEntityBy13.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                    }
                                }
                                else
                                {
                                    pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                }
                                break;
                            #endregion
                            #region 调拨出库库存更新
                            case (int)TriggerType.AllotOutStock:
                                var stockOrderDetailEntityBy6 = stockOrderDetailLogic.GetSingleById(item.DataId);
                                if (stockOrderDetailEntityBy6 != null &&
                                    !string.IsNullOrEmpty(stockOrderDetailEntityBy6.ORGCODE) &&
                                    !string.IsNullOrEmpty(stockOrderDetailEntityBy6.GOODSCODE) &&
                                    stockOrderDetailEntityBy6.QTY.HasValue)
                                {
                                    //检查是否为标品销售
                                    var stListByMother = skuTransformLogic.GetListByMotherCode(stockOrderDetailEntityBy6.GOODSCODE);
                                    var stListByChild = skuTransformLogic.GetListByChildCode(stockOrderDetailEntityBy6.GOODSCODE);
                                    if (stListByMother.Count == 0 && stListByChild.Count == 0)
                                    {
                                        var qtyDTO = new SKUQTYChangeDTO();
                                        qtyDTO.Id = item.Id;
                                        qtyDTO.Type = (int)TriggerType.AllotOutStock;
                                        qtyDTO.ShopCode = stockOrderDetailEntityBy6.ORGCODE;
                                        qtyDTO.SKUCode = stockOrderDetailEntityBy6.GOODSCODE;
                                        qtyDTO.Qty = stockOrderDetailEntityBy6.QTY.Value;
                                        qtyDTO.isSuccess = false;
                                        qtyList.Add(qtyDTO);
                                    }
                                    else
                                    {
                                        foreach (var st in stListByMother)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.AllotOutStock;
                                            qtyDTO.ShopCode = stockOrderDetailEntityBy6.ORGCODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(stockOrderDetailEntityBy6.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                        foreach (var st in stListByChild)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.AllotOutStock;
                                            qtyDTO.ShopCode = stockOrderDetailEntityBy6.ORGCODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(stockOrderDetailEntityBy6.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                    }
                                }
                                else
                                {
                                    pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                }
                                break;
                            #endregion
                            #region 退货出库库存更新
                            case (int)TriggerType.DeliveryCancelOutStock:
                                var stockOrderDetailEntityBy4 = stockOrderDetailLogic.GetSingleById(item.DataId);
                                if (stockOrderDetailEntityBy4 != null &&
                                    !string.IsNullOrEmpty(stockOrderDetailEntityBy4.ORGCODE) &&
                                    !string.IsNullOrEmpty(stockOrderDetailEntityBy4.GOODSCODE) &&
                                    stockOrderDetailEntityBy4.QTY.HasValue)
                                {
                                    //检查是否为标品销售
                                    var stListByMother = skuTransformLogic.GetListByMotherCode(stockOrderDetailEntityBy4.GOODSCODE);
                                    var stListByChild = skuTransformLogic.GetListByChildCode(stockOrderDetailEntityBy4.GOODSCODE);
                                    if (stListByMother.Count == 0 && stListByChild.Count == 0)
                                    {
                                        var qtyDTO = new SKUQTYChangeDTO();
                                        qtyDTO.Id = item.Id;
                                        qtyDTO.Type = (int)TriggerType.DeliveryCancelOutStock;
                                        qtyDTO.ShopCode = stockOrderDetailEntityBy4.ORGCODE;
                                        qtyDTO.SKUCode = stockOrderDetailEntityBy4.GOODSCODE;
                                        qtyDTO.Qty = stockOrderDetailEntityBy4.QTY.Value;
                                        qtyDTO.isSuccess = false;
                                        qtyList.Add(qtyDTO);
                                    }
                                    else
                                    {
                                        foreach (var st in stListByMother)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.DeliveryCancelOutStock;
                                            qtyDTO.ShopCode = stockOrderDetailEntityBy4.ORGCODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(stockOrderDetailEntityBy4.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                        foreach (var st in stListByChild)
                                        {
                                            var qtyDTO = new SKUQTYChangeDTO();
                                            qtyDTO.Id = item.Id;
                                            qtyDTO.Type = (int)TriggerType.DeliveryCancelOutStock;
                                            qtyDTO.ShopCode = stockOrderDetailEntityBy4.ORGCODE;
                                            qtyDTO.SKUCode = st.CHILDSKUCODE;
                                            qtyDTO.Qty = Math.Floor(stockOrderDetailEntityBy4.QTY.Value / st.QTYPERMOTHER.Value);
                                            qtyDTO.isSuccess = false;
                                            qtyList.Add(qtyDTO);
                                        }
                                    }
                                }
                                else
                                {
                                    pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                }
                                break;
                            #endregion
                            #region 商品子母码绑定转换变更库存更新
                            case (int)TriggerType.SKUTransformStock:
                                long skuTransformIdByTF = 0;
                                if (long.TryParse(item.DataId, out skuTransformIdByTF))
                                {
                                    var skuTransformEntity = skuTransformLogic.GetSingleById(skuTransformIdByTF);
                                    if (skuTransformEntity != null &&
                                        !string.IsNullOrEmpty(skuTransformEntity.MOTHERSKUCODE) &&
                                        !string.IsNullOrEmpty(skuTransformEntity.CHILDSKUCODE) &&
                                        skuTransformEntity.QTYPERMOTHER.HasValue &&
                                        skuTransformEntity.QTYPERMOTHER.Value > 0)
                                    {
                                        SKUQTYPERChangeDTO skuDto = new SKUQTYPERChangeDTO();
                                        skuDto.Id = item.Id;
                                        skuDto.SKUCode = skuTransformEntity.CHILDSKUCODE;
                                        skuDto.QtyPerBefore = item.ChangeValue;
                                        skuDto.QtyPerAfter = skuTransformEntity.QTYPERMOTHER.Value;
                                        skuDto.isSuccess = false;

                                        tfList.Add(skuDto);
                                    }
                                    else
                                    {
                                        pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                    }
                                }
                                else
                                {
                                    pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                }
                                break;
                            #endregion
                            #region 商品转换率绑定库存更新
                            case (int)TriggerType.SKUPerStock:
                                long skuTransformIdByPer = 0;
                                if (long.TryParse(item.DataId, out skuTransformIdByPer))
                                {
                                    var skuTransformEntity = skuTransformLogic.GetSingleById(skuTransformIdByPer);
                                    if (skuTransformEntity != null &&
                                        !string.IsNullOrEmpty(skuTransformEntity.MOTHERSKUCODE) &&
                                        !string.IsNullOrEmpty(skuTransformEntity.CHILDSKUCODE) &&
                                        skuTransformEntity.QTYPERMOTHER.HasValue &&
                                        skuTransformEntity.QTYPERMOTHER.Value > 0)
                                    {
                                        SKUQTYPERChangeDTO skuDto = new SKUQTYPERChangeDTO();
                                        skuDto.Id = item.Id;
                                        skuDto.SKUCode = skuTransformEntity.CHILDSKUCODE;
                                        skuDto.QtyPerBefore = item.ChangeValue;
                                        skuDto.QtyPerAfter = skuTransformEntity.QTYPERMOTHER.Value;
                                        skuDto.isSuccess = false;

                                        perList.Add(skuDto);
                                    }
                                    else
                                    {
                                        pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                    }
                                }
                                else
                                {
                                    pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                }
                                break;
                            #endregion
                        }
                    }

                    #region 价格
                    if (childSKUPriceList.Count > 0)
                    {
                        shopGoodsLogic.UpdateGoodsPrice(childSKUPriceList);

                        foreach (var item in childSKUPriceList)
                        {
                            if (item.isSuccess)
                            {
                                var result = pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                if (result)
                                {
                                    Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " price update success");
                                }
                                else
                                {
                                    Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " price update failure");
                                }
                            }
                        }
                    }
                    if (motherSKUPriceList.Count > 0)
                    {
                        goodsPriceAlermLogic.Add(motherSKUPriceList);

                        foreach (var item in motherSKUPriceList)
                        {
                            if (item.isSuccess)
                            {
                                var result = pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                if (result)
                                {
                                    Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " price update record write success");
                                }
                                else
                                {
                                    Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " price update record write failure");
                                }
                            }
                        }
                    }
                    #endregion
                    #region 状态
                    if (statusList.Count > 0)
                    {
                        shopGoodsLogic.UpdateGoodsStatus(statusList);

                        foreach (var item in statusList)
                        {
                            if (item.isSuccess)
                            {
                                var result = pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                if (result)
                                {
                                    Console.WriteLine("goods: " + item.SKUCode + " status update success");
                                }
                                else
                                {
                                    Console.WriteLine("goods: " + item.SKUCode + " status update failure");
                                }
                            }
                        }
                    }
                    #endregion
                    #region 库存
                    if (qtyList.Count > 0)
                    {
                        shopGoodsLogic.UpdateGoodsQty(qtyList);

                        foreach (var item in qtyList)
                        {
                            if (item.isSuccess)
                            {
                                var result = pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                if (result)
                                {
                                    switch (item.Type)
                                    {
                                        case (int)TriggerType.DistributeInStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " distribute in stock update success");
                                            break;
                                        case (int)TriggerType.AllotInStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " allot in stock update success");
                                            break;
                                        case (int)TriggerType.DeliveryInStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " delivery in stock update success");
                                            break;
                                        case (int)TriggerType.POSCancelInStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " pos cancel in stock update success");
                                            break;
                                        case (int)TriggerType.POSSaleOutStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " pos sale out stock update success");
                                            break;
                                        case (int)TriggerType.LossOutStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " loss out stock update success");
                                            break;
                                        case (int)TriggerType.AllotOutStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " allot out stock update success");
                                            break;
                                        case (int)TriggerType.DeliveryCancelOutStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " delivery cancel out stock update success");
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (item.Type)
                                    {
                                        case (int)TriggerType.DistributeInStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " distribute in stock update failure");
                                            break;
                                        case (int)TriggerType.AllotInStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " allot in stock update failure");
                                            break;
                                        case (int)TriggerType.DeliveryInStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " delivery in stock update failure");
                                            break;
                                        case (int)TriggerType.POSCancelInStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " pos cancel in stock update failure");
                                            break;
                                        case (int)TriggerType.POSSaleOutStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " pos sale out stock update failure");
                                            break;
                                        case (int)TriggerType.LossOutStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " loss out stock update failure");
                                            break;
                                        case (int)TriggerType.AllotOutStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " allot out stock update failure");
                                            break;
                                        case (int)TriggerType.DeliveryCancelOutStock:
                                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " delivery cancel out stock update failure");
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region 转换绑定
                    if (tfList.Count > 0)
                    {
                        shopGoodsLogic.UpdateGoodsInventoryByTransform(tfList);

                        foreach (var item in tfList)
                        {
                            if (item.isSuccess)
                            {
                                var result = pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                if (result)
                                {
                                    Console.WriteLine("goods: " + item.SKUCode + " transform bind update inventory success");
                                }
                                else
                                {
                                    Console.WriteLine("goods: " + item.SKUCode + " transform bind update inventory failure");
                                }
                            }
                        }
                    }
                    #region 转换率
                    if (perList.Count > 0)
                    {
                        shopGoodsLogic.UpdateGoodsInventoryByQtyPer(perList);

                        foreach (var item in perList)
                        {
                            if (item.isSuccess)
                            {
                                var result = pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
                                if (result)
                                {
                                    Console.WriteLine("goods: " + item.SKUCode + " qty per update inventory success");
                                }
                                else
                                {
                                    Console.WriteLine("goods: " + item.SKUCode + " qty per update inventory failure");
                                }
                            }
                        }
                    }
                    #endregion
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.ToString());
            }
        }


        #region
        //public void Process1()
        //{
        //    try
        //    {
        //        Console.WriteLine("Whether to update goods prices in full[y/n]?");
        //        var readByPrice = Console.ReadLine();

        //        Console.WriteLine("Whether to statistics goods inventories from the closing date[y/n]?");
        //        var readByStock = Console.ReadLine();

        //        PP_ShopLogic shopLogic = new PP_ShopLogic();
        //        PP_ShopGoodsLogic shopGoodsLogic = new PP_ShopGoodsLogic();
        //        PP_TriggerDataLoigc pp_TriggerDataLogic = new PP_TriggerDataLoigc();
        //        OrganizeSKULogic organizeSKULogic = new OrganizeSKULogic();
        //        StockLogic stockLogic = new StockLogic();
        //        StockBakLogic stockBakLogic = new StockBakLogic();
        //        StockSnapshotLogic stockSnapshotLogic = new StockSnapshotLogic();
        //        SKUTransformLogic skuTransformLogic = new SKUTransformLogic();
        //        POSV2_SaleOrderDetailLogic posv2_SaleOrderDetailLogic = new POSV2_SaleOrderDetailLogic();

        //        //根据线上门店获取商品的价格及库存
        //        var shopList = shopLogic.GetList(1);

        //        #region 是否全量更新商品价格
        //        if (readByPrice.Equals("y", StringComparison.InvariantCultureIgnoreCase))
        //        {
        //            foreach (var shop in shopList)
        //            {
        //                #region 获取价格信息
        //                OrganizeSKULogic skuLogic = new OrganizeSKULogic();
        //                int count = 0;
        //                int totalCount = 0;
        //                int pageIndex = 0;
        //                int pageSize = 100;
        //                while (count <= totalCount)
        //                {
        //                    var skuDTOList = skuLogic.GetPagerList(1, shop.shopCode, "", out totalCount, pageIndex, pageSize);
        //                    pageIndex++;
        //                    count = (pageIndex) * pageSize;
        //                    var result = shopGoodsLogic.UpdateGoodsPrice(skuDTOList);
        //                    if (!result)
        //                    {
        //                        break;
        //                    }
        //                }
        //                Console.WriteLine("Shop:" + shop.shopCode + ", goods price update success");
        //                #endregion
        //            }
        //        }
        //        #endregion

        //        #region 是否从日结日统计商品库存
        //        if (readByStock.Equals("y", StringComparison.InvariantCultureIgnoreCase))
        //        {
        //            #region 每次运行都根据日结信息获取所有门店所有商品的真实库存信息(根据日结库存统计到当前pos机订单数据)
        //            List<SKUStockChangeDTO> stockDTOList = new List<SKUStockChangeDTO>();
        //            foreach (var shop in shopList)
        //            {
        //                Console.WriteLine("shop: " + shop.shopCode + ", start statistics real inventory");

        //                //第一步: 获取所有商品的库存信息
        //                List<Stock> stockList = new List<Stock>();
        //                var stockbakList = stockBakLogic.GetList(shop.shopCode);
        //                if (stockbakList.Count == 0)
        //                {
        //                    stockList = stockLogic.GetList(shop.shopCode);
        //                    Console.WriteLine("Shop:" + shop.shopCode + ", goods count:" + stockList.Count);

        //                    //第二步: 获取最后日结信息
        //                    var stockSnapshotList = stockSnapshotLogic.GetLastStockSnapshot(shop.shopCode);
        //                    Console.WriteLine("Shop:" + shop.shopCode + ", snapshot goods count: " + stockSnapshotList.Count);
        //                    //日结的商品信息
        //                    var goodsList = stockSnapshotList.Select(x => x.GOODSCODE);

        //                    //第三步: 根据日结的商品信息更新为真实库存
        //                    foreach (var stock in stockList)
        //                    {
        //                        if (goodsList.Contains(stock.GOODSCODE))
        //                        {
        //                            //获取商品的日结库存
        //                            var ssModel = stockSnapshotList.Where(x => x.GOODSCODE.Equals(stock.GOODSCODE)).FirstOrDefault();
        //                            var skuQty = ssModel == null ? 0 : ssModel.QTY.HasValue ? ssModel.QTY.Value : 0;

        //                            //获取从日结开始到当前的收银订单统计
        //                            var statisticsDict = posv2_SaleOrderDetailLogic.GetStatisticsByOrgAndSKU(shop.shopCode, stock.GOODSCODE, ssModel.BACKUPDATE, DateTime.Now.AddDays(1).Date.AddSeconds(-1));

        //                            //更新为真实库存
        //                            stock.QTY = skuQty +
        //                                        (stock.INQTYDAY.HasValue ? stock.INQTYDAY.Value : 0) -
        //                                        statisticsDict[(int)POSV2_SaleOrderType.Sale] +
        //                                        statisticsDict[(int)POSV2_SaleOrderType.Return] -
        //                                        statisticsDict[(int)POSV2_SaleOrderType.Loss];
        //                        }

        //                        #region
        //                        StockBak entity = new StockBak();
        //                        entity.ID = stock.ID;
        //                        entity.ORGID = stock.ORGID;
        //                        entity.ORGCODE = stock.ORGCODE;
        //                        entity.STORAGEID = stock.STORAGEID;
        //                        entity.STORAGECODE = stock.STORAGECODE;
        //                        entity.GOODSID = stock.GOODSID;
        //                        entity.GOODSCODE = stock.GOODSCODE;
        //                        entity.PRICE = stock.PRICE;
        //                        entity.PASSAGEQTY = stock.PASSAGEQTY;
        //                        entity.QTY = stock.QTY;
        //                        entity.AMT = stock.AMT;
        //                        entity.ENDINDATE = stock.ENDINDATE;
        //                        entity.ENDSALEDATE = stock.ENDSALEDATE;
        //                        entity.ENDINVENTORYDATE = stock.ENDINVENTORYDATE;
        //                        entity.INQTYDAY = stock.INQTYDAY;
        //                        entity.LOSSQTYDAY = stock.LOSSQTYDAY;
        //                        entity.SALEQTYDAY = stock.SALEQTYDAY;
        //                        entity.INQTY1DAY = stock.INQTY1DAY;
        //                        entity.LOSSQTY1DAY = stock.LOSSQTY1DAY;
        //                        entity.SALEQTY1DAY = stock.SALEQTY1DAY;
        //                        entity.INQTY3DAY = stock.INQTY3DAY;
        //                        entity.SALEQTY3DAY = stock.SALEQTY3DAY;
        //                        entity.LOSSQTY3DAY = stock.LOSSQTY3DAY;
        //                        entity.INQTY7DAY = stock.INQTY7DAY;
        //                        entity.SALEQTY7DAY = stock.SALEQTY7DAY;
        //                        entity.LOSSQTY7DAY = stock.LOSSQTY7DAY;
        //                        entity.INQTY28DAY = stock.INQTY28DAY;
        //                        entity.SALEQTY28DAY = stock.SALEQTY28DAY;
        //                        entity.LOSSQTY28DAY = stock.LOSSQTY28DAY;
        //                        entity.STATUS = stock.STATUS;
        //                        entity.DESCRIPTION = stock.DESCRIPTION;
        //                        entity.CREATEBYID = stock.CREATEBYID;
        //                        entity.CREATEON = stock.CREATEON;
        //                        entity.CREATEBYNAME = stock.CREATEBYNAME;
        //                        entity.MODIFYBYID = stock.MODIFYBYID;
        //                        entity.MODIFYBYNAME = stock.MODIFYBYNAME;
        //                        entity.MODIFYON = stock.MODIFYON;
        //                        entity.MAXSTOCKQTY = stock.MAXSTOCKQTY;
        //                        entity.MINSTOCKQTY = stock.MINSTOCKQTY;
        //                        entity.SALEQTYTODAY = stock.SALEQTYTODAY;
        //                        entity.REALQTY = stock.REALQTY;
        //                        entity.LOCKQTY = stock.LOCKQTY;
        //                        entity.PREQTY = stock.PREQTY;
        //                        stockBakLogic.Insert(entity);
        //                        #endregion
        //                    }

        //                    //第四步: 获取所有非标品
        //                    var notStandardSKUList = skuTransformLogic.GetList(stockList.Select(x => x.GOODSCODE).ToList());
        //                    var notStandardSKUListByMotherCode = notStandardSKUList.Select(x => x.MOTHERSKUCODE);
        //                    var notStandardSKUListByChildCode = notStandardSKUList.Select(x => x.CHILDSKUCODE);

        //                    //第五步: 从母码全部转换为子码库存（标品看作是子码， 非标品根据转换率换算）
        //                    var standardStockList = stockList.Where(x => !notStandardSKUListByMotherCode.Contains(x.GOODSCODE)).Where(x => !notStandardSKUListByChildCode.Contains(x.GOODSCODE)).ToList();
        //                    var notStandardStockList = stockList.Except(standardStockList, new StockListEquality()).ToList();

        //                    //非标品里的母码(有库存)
        //                    var notStandardMotherCodeStockList = notStandardStockList.Where(x => notStandardSKUListByMotherCode.Contains(x.GOODSCODE) && x.QTY > 0).ToList();

        //                    //从非标品里的母码找出关联的所有子码，并根据转换率计算子码的库存，如果子码含有库存，则加上
        //                    foreach (var motherCodeStock in notStandardMotherCodeStockList)
        //                    {
        //                        Stock item = motherCodeStock;
        //                        var childList = skuTransformLogic.GetChildList(item.GOODSCODE);
        //                        foreach (var child in childList)
        //                        {
        //                            var childStockModel = notStandardStockList.Where(x => x.GOODSCODE.Equals(child.CHILDSKUCODE)).FirstOrDefault();

        //                            decimal childQty = Math.Floor(motherCodeStock.QTY.Value / child.QTYPERMOTHER.Value);
        //                            item.GOODSCODE = child.CHILDSKUCODE;
        //                            item.QTY = childQty + (childStockModel.QTY.HasValue ? childStockModel.QTY.Value : 0);
        //                            standardStockList.Add(item);
        //                        }
        //                    }

        //                    //转换
        //                    stockDTOList = Mapper.Map<List<SKUStockChangeDTO>>(standardStockList);
        //                    var result = shopGoodsLogic.UpdateGoodsInventory(stockDTOList);
        //                    if (!result)
        //                    {
        //                        break;
        //                    }
        //                    Console.WriteLine("Shop: " + shop.shopCode + " inventory update succeses");
        //                }
        //                else
        //                {
        //                    foreach (var stockbak in stockbakList)
        //                    {
        //                        #region
        //                        Stock stock = new Stock();
        //                        stock.ID = stockbak.ID;
        //                        stock.ORGID = stockbak.ORGID;
        //                        stock.ORGCODE = stockbak.ORGCODE;
        //                        stock.STORAGEID = stockbak.STORAGEID;
        //                        stock.STORAGECODE = stockbak.STORAGECODE;
        //                        stock.GOODSID = stockbak.GOODSID;
        //                        stock.GOODSCODE = stockbak.GOODSCODE;
        //                        stock.PRICE = stockbak.PRICE;
        //                        stock.PASSAGEQTY = stockbak.PASSAGEQTY;
        //                        stock.QTY = stockbak.QTY;
        //                        stock.AMT = stockbak.AMT;
        //                        stock.ENDINDATE = stockbak.ENDINDATE;
        //                        stock.ENDSALEDATE = stockbak.ENDSALEDATE;
        //                        stock.ENDINVENTORYDATE = stockbak.ENDINVENTORYDATE;
        //                        stock.INQTYDAY = stockbak.INQTYDAY;
        //                        stock.LOSSQTYDAY = stockbak.LOSSQTYDAY;
        //                        stock.SALEQTYDAY = stockbak.SALEQTYDAY;
        //                        stock.INQTY1DAY = stockbak.INQTY1DAY;
        //                        stock.LOSSQTY1DAY = stockbak.LOSSQTY1DAY;
        //                        stock.SALEQTY1DAY = stockbak.SALEQTY1DAY;
        //                        stock.INQTY3DAY = stockbak.INQTY3DAY;
        //                        stock.SALEQTY3DAY = stockbak.SALEQTY3DAY;
        //                        stock.LOSSQTY3DAY = stockbak.LOSSQTY3DAY;
        //                        stock.INQTY7DAY = stockbak.INQTY7DAY;
        //                        stock.SALEQTY7DAY = stockbak.SALEQTY7DAY;
        //                        stock.LOSSQTY7DAY = stockbak.LOSSQTY7DAY;
        //                        stock.INQTY28DAY = stockbak.INQTY28DAY;
        //                        stock.SALEQTY28DAY = stockbak.SALEQTY28DAY;
        //                        stock.LOSSQTY28DAY = stockbak.LOSSQTY28DAY;
        //                        stock.STATUS = stockbak.STATUS;
        //                        stock.DESCRIPTION = stockbak.DESCRIPTION;
        //                        stock.CREATEBYID = stockbak.CREATEBYID;
        //                        stock.CREATEON = stockbak.CREATEON;
        //                        stock.CREATEBYNAME = stockbak.CREATEBYNAME;
        //                        stock.MODIFYBYID = stockbak.MODIFYBYID;
        //                        stock.MODIFYBYNAME = stockbak.MODIFYBYNAME;
        //                        stock.MODIFYON = stockbak.MODIFYON;
        //                        stock.MAXSTOCKQTY = stockbak.MAXSTOCKQTY;
        //                        stock.MINSTOCKQTY = stockbak.MINSTOCKQTY;
        //                        stock.SALEQTYTODAY = stockbak.SALEQTYTODAY;
        //                        stock.REALQTY = stockbak.REALQTY;
        //                        stock.LOCKQTY = stockbak.LOCKQTY;
        //                        stock.PREQTY = stockbak.PREQTY;
        //                        stockList.Add(stock);
        //                        #endregion
        //                    }

        //                    //第四步: 获取所有非标品
        //                    var notStandardSKUList = skuTransformLogic.GetList(stockList.Select(x => x.GOODSCODE).ToList());
        //                    var notStandardSKUListByMotherCode = notStandardSKUList.Select(x => x.MOTHERSKUCODE);
        //                    var notStandardSKUListByChildCode = notStandardSKUList.Select(x => x.CHILDSKUCODE);

        //                    //第五步: 从母码全部转换为子码库存（标品看作是子码， 非标品根据转换率换算）
        //                    var standardStockList = stockList.Where(x => !notStandardSKUListByMotherCode.Contains(x.GOODSCODE)).Where(x => !notStandardSKUListByChildCode.Contains(x.GOODSCODE)).ToList();
        //                    var notStandardStockList = stockList.Except(standardStockList, new StockListEquality()).ToList();

        //                    //非标品里的母码(有库存)
        //                    var notStandardMotherCodeStockList = notStandardStockList.Where(x => notStandardSKUListByMotherCode.Contains(x.GOODSCODE) && x.QTY > 0).ToList();

        //                    //从非标品里的母码找出关联的所有子码，并根据转换率计算子码的库存，如果子码含有库存，则加上
        //                    foreach (var motherCodeStock in notStandardMotherCodeStockList)
        //                    {
        //                        if (motherCodeStock.GOODSCODE.Equals("10030012"))
        //                        {
        //                            Stock item = motherCodeStock;
        //                            var childList = skuTransformLogic.GetChildList(item.GOODSCODE);
        //                            foreach (var child in childList)
        //                            {
        //                                var childStockModel = notStandardStockList.Where(x => x.GOODSCODE.Equals(child.CHILDSKUCODE)).FirstOrDefault();
        //                                if (childStockModel == null)
        //                                {
        //                                    string a = string.Empty;
        //                                }
        //                                decimal childQty = Math.Floor(motherCodeStock.QTY.Value / child.QTYPERMOTHER.Value);
        //                                item.GOODSCODE = child.CHILDSKUCODE;
        //                                item.QTY = childQty + (childStockModel.QTY.HasValue ? childStockModel.QTY.Value : 0);
        //                                standardStockList.Add(item);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            Stock item = motherCodeStock;
        //                            var childList = skuTransformLogic.GetChildList(item.GOODSCODE);
        //                            foreach (var child in childList)
        //                            {
        //                                var childStockModel = notStandardStockList.Where(x => x.GOODSCODE.Equals(child.CHILDSKUCODE)).FirstOrDefault();
        //                                if (childStockModel != null)
        //                                {
        //                                    decimal childQty = Math.Floor(motherCodeStock.QTY.Value / child.QTYPERMOTHER.Value);
        //                                    item.GOODSCODE = child.CHILDSKUCODE;
        //                                    item.QTY = childQty + (childStockModel.QTY.HasValue ? childStockModel.QTY.Value : 0);
        //                                    standardStockList.Add(item);
        //                                }
        //                            }
        //                        }
        //                    }

        //                    //转换
        //                    stockDTOList = Mapper.Map<List<SKUStockChangeDTO>>(standardStockList);
        //                    var result = shopGoodsLogic.UpdateGoodsInventory(stockDTOList);
        //                    if (!result)
        //                    {
        //                        break;
        //                    }
        //                    Console.WriteLine("Shop: " + shop.shopCode + " inventory update succeses");
        //                }
        //            }
        //            #endregion
        //        }
        //        #endregion

        //        #region 从触发数据中更新库存
        //        while (true)
        //        {
        //            var priceList = new List<SKUPriceChangeDTO>();
        //            var qtyList = new List<SKUQTYDTO>();
        //            var statusList = new List<SKUStatusChangeDTO>();
        //            var perList = new List<SKUQTYPERChangeDTO>();

        //            var list = pp_TriggerDataLogic.GetList(false, false);
        //            foreach (var item in list)
        //            {
        //                switch (item.Type)
        //                {
        //                    #region 商品价格更新
        //                    case (int)TriggerType.Price:
        //                        long orgSkuIdByPrice = 0;
        //                        if (long.TryParse(item.DataId, out orgSkuIdByPrice))
        //                        {
        //                            var orgSkuEntity = organizeSKULogic.GetSingleById(orgSkuIdByPrice);
        //                            if (orgSkuEntity != null &&
        //                                !string.IsNullOrEmpty(orgSkuEntity.ORGANIZE_CODE) &&
        //                                !string.IsNullOrEmpty(orgSkuEntity.SKU_CODE) &&
        //                                orgSkuEntity.SPRICE.HasValue)
        //                            {
        //                                OrganizeSKUDTO skuDto = new OrganizeSKUDTO();
        //                                skuDto.triggerData = item;
        //                                skuDto.sku = orgSkuEntity;

        //                                priceList.Add(Mapper.Map<SKUPriceChangeDTO>(skuDto));
        //                            }
        //                            else
        //                            {
        //                                pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
        //                        }
        //                        break;
        //                    #endregion
        //                    #region 入库后库存更新
        //                    case (int)TriggerType.InStock:
        //                        var stockEntity = stockLogic.GetSingleById(item.DataId);
        //                        if (stockEntity != null &&
        //                            !string.IsNullOrEmpty(stockEntity.ORGCODE) &&
        //                            !string.IsNullOrEmpty(stockEntity.GOODSCODE) &&
        //                            stockEntity.INQTYDAY.HasValue)
        //                        {
        //                            //检查是否为标品销售
        //                            var stListByMother = skuTransformLogic.GetListByMotherCode(stockEntity.GOODSCODE);
        //                            var stListByChild = skuTransformLogic.GetListByChildCode(stockEntity.GOODSCODE);
        //                            if (stListByMother.Count == 0 && stListByChild.Count == 0)
        //                            {
        //                                var qtyDTO = new SKUQTYDTO();
        //                                qtyDTO.Id = item.Id;
        //                                qtyDTO.Type = (int)TriggerType.InStock;
        //                                qtyDTO.ShopCode = stockEntity.ORGCODE;
        //                                qtyDTO.SKUCode = stockEntity.GOODSCODE;
        //                                qtyDTO.Qty = item.ChangeValue;
        //                                qtyDTO.isSuccess = false;
        //                                qtyList.Add(qtyDTO);
        //                            }
        //                            else
        //                            {
        //                                foreach (var st in stListByMother)
        //                                {
        //                                    var qtyDTO = new SKUQTYDTO();
        //                                    qtyDTO.Id = item.Id;
        //                                    qtyDTO.Type = (int)TriggerType.InStock;
        //                                    qtyDTO.ShopCode = stockEntity.ORGCODE;
        //                                    qtyDTO.SKUCode = st.CHILDSKUCODE;
        //                                    qtyDTO.Qty = Math.Floor(item.ChangeValue / st.QTYPERMOTHER.Value);
        //                                    qtyDTO.isSuccess = false;
        //                                    qtyList.Add(qtyDTO);
        //                                }
        //                                foreach (var st in stListByChild)
        //                                {
        //                                    var qtyDTO = new SKUQTYDTO();
        //                                    qtyDTO.Id = item.Id;
        //                                    qtyDTO.Type = (int)TriggerType.InStock;
        //                                    qtyDTO.ShopCode = stockEntity.ORGCODE;
        //                                    qtyDTO.SKUCode = st.CHILDSKUCODE;
        //                                    qtyDTO.Qty = Math.Floor(item.ChangeValue / st.QTYPERMOTHER.Value);
        //                                    qtyDTO.isSuccess = false;
        //                                    qtyList.Add(qtyDTO);
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
        //                        }
        //                        break;
        //                    #endregion
        //                    #region 销售后库存更新
        //                    case (int)TriggerType.SaleStock:
        //                        var orderDetailBySaleEntity = posv2_SaleOrderDetailLogic.GetSingleById(item.DataId);
        //                        if (orderDetailBySaleEntity != null &&
        //                            !string.IsNullOrEmpty(orderDetailBySaleEntity.STORECODE) &&
        //                            !string.IsNullOrEmpty(orderDetailBySaleEntity.GOODSCODE) &&
        //                            orderDetailBySaleEntity.ORIGINALQTY.HasValue &&
        //                            orderDetailBySaleEntity.ORIGINALQTY.Value > 0)
        //                        {
        //                            //检查是否为标品销售
        //                            var stListByMother = skuTransformLogic.GetListByMotherCode(orderDetailBySaleEntity.GOODSCODE);
        //                            var stListByChild = skuTransformLogic.GetListByChildCode(orderDetailBySaleEntity.GOODSCODE);
        //                            if (stListByMother.Count == 0 && stListByChild.Count == 0)
        //                            {
        //                                var qtyDTO = new SKUQTYDTO();
        //                                qtyDTO.Id = item.Id;
        //                                qtyDTO.Type = (int)TriggerType.SaleStock;
        //                                qtyDTO.ShopCode = orderDetailBySaleEntity.STORECODE;
        //                                qtyDTO.SKUCode = orderDetailBySaleEntity.GOODSCODE;
        //                                qtyDTO.Qty = orderDetailBySaleEntity.ORIGINALQTY.Value;
        //                                qtyDTO.isSuccess = false;
        //                                qtyList.Add(qtyDTO);
        //                            }
        //                            else
        //                            {
        //                                foreach (var st in stListByMother)
        //                                {
        //                                    var qtyDTO = new SKUQTYDTO();
        //                                    qtyDTO.Id = item.Id;
        //                                    qtyDTO.Type = (int)TriggerType.SaleStock;
        //                                    qtyDTO.ShopCode = orderDetailBySaleEntity.STORECODE;
        //                                    qtyDTO.SKUCode = st.CHILDSKUCODE;
        //                                    qtyDTO.Qty = Math.Floor(orderDetailBySaleEntity.ORIGINALQTY.Value / st.QTYPERMOTHER.Value);
        //                                    qtyDTO.isSuccess = false;
        //                                    qtyList.Add(qtyDTO);
        //                                }
        //                                foreach (var st in stListByChild)
        //                                {
        //                                    var qtyDTO = new SKUQTYDTO();
        //                                    qtyDTO.Id = item.Id;
        //                                    qtyDTO.Type = (int)TriggerType.SaleStock;
        //                                    qtyDTO.ShopCode = orderDetailBySaleEntity.STORECODE;
        //                                    qtyDTO.SKUCode = st.CHILDSKUCODE;
        //                                    qtyDTO.Qty = Math.Floor(orderDetailBySaleEntity.ORIGINALQTY.Value / st.QTYPERMOTHER.Value);
        //                                    qtyDTO.isSuccess = false;
        //                                    qtyList.Add(qtyDTO);
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
        //                        }
        //                        break;
        //                    #endregion
        //                    #region 退货后库存更新
        //                    case (int)TriggerType.ReturnStock:
        //                        var orderDetailByReturnEntity = posv2_SaleOrderDetailLogic.GetSingleById(item.DataId);
        //                        if (orderDetailByReturnEntity != null &&
        //                            !string.IsNullOrEmpty(orderDetailByReturnEntity.STORECODE) &&
        //                            !string.IsNullOrEmpty(orderDetailByReturnEntity.GOODSCODE) &&
        //                            orderDetailByReturnEntity.ORIGINALQTY.HasValue &&
        //                            orderDetailByReturnEntity.ORIGINALQTY.Value > 0)
        //                        {
        //                            //检查是否为标品销售
        //                            var stListByMother = skuTransformLogic.GetListByMotherCode(orderDetailByReturnEntity.GOODSCODE);
        //                            var stListByChild = skuTransformLogic.GetListByChildCode(orderDetailByReturnEntity.GOODSCODE);
        //                            if (stListByMother.Count == 0 && stListByChild.Count == 0)
        //                            {
        //                                var qtyDTO = new SKUQTYDTO();
        //                                qtyDTO.Id = item.Id;
        //                                qtyDTO.Type = (int)TriggerType.ReturnStock;
        //                                qtyDTO.ShopCode = orderDetailByReturnEntity.STORECODE;
        //                                qtyDTO.SKUCode = orderDetailByReturnEntity.GOODSCODE;
        //                                qtyDTO.Qty = orderDetailByReturnEntity.ORIGINALQTY.Value;
        //                                qtyDTO.isSuccess = false;
        //                                qtyList.Add(qtyDTO);
        //                            }
        //                            else
        //                            {
        //                                foreach (var st in stListByMother)
        //                                {
        //                                    var qtyDTO = new SKUQTYDTO();
        //                                    qtyDTO.Id = item.Id;
        //                                    qtyDTO.Type = (int)TriggerType.ReturnStock;
        //                                    qtyDTO.ShopCode = orderDetailByReturnEntity.STORECODE;
        //                                    qtyDTO.SKUCode = st.CHILDSKUCODE;
        //                                    qtyDTO.Qty = Math.Floor(orderDetailByReturnEntity.ORIGINALQTY.Value / st.QTYPERMOTHER.Value);
        //                                    qtyDTO.isSuccess = false;
        //                                    qtyList.Add(qtyDTO);
        //                                }
        //                                foreach (var st in stListByChild)
        //                                {
        //                                    var qtyDTO = new SKUQTYDTO();
        //                                    qtyDTO.Id = item.Id;
        //                                    qtyDTO.Type = (int)TriggerType.ReturnStock;
        //                                    qtyDTO.ShopCode = orderDetailByReturnEntity.STORECODE;
        //                                    qtyDTO.SKUCode = st.CHILDSKUCODE;
        //                                    qtyDTO.Qty = Math.Floor(orderDetailByReturnEntity.ORIGINALQTY.Value / st.QTYPERMOTHER.Value);
        //                                    qtyDTO.isSuccess = false;
        //                                    qtyList.Add(qtyDTO);
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
        //                        }
        //                        break;
        //                    #endregion
        //                    #region 报损后库存更新
        //                    case (int)TriggerType.LossStock:
        //                        var orderDetailByLossEntity = posv2_SaleOrderDetailLogic.GetSingleById(item.DataId);
        //                        if (orderDetailByLossEntity != null &&
        //                            !string.IsNullOrEmpty(orderDetailByLossEntity.STORECODE) &&
        //                            !string.IsNullOrEmpty(orderDetailByLossEntity.GOODSCODE) &&
        //                            orderDetailByLossEntity.ORIGINALQTY.HasValue &&
        //                            orderDetailByLossEntity.ORIGINALQTY.Value > 0)
        //                        {
        //                            //检查是否为标品销售
        //                            var stListByMother = skuTransformLogic.GetListByMotherCode(orderDetailByLossEntity.GOODSCODE);
        //                            var stListByChild = skuTransformLogic.GetListByChildCode(orderDetailByLossEntity.GOODSCODE);
        //                            if (stListByMother.Count == 0 && stListByChild.Count == 0)
        //                            {
        //                                var qtyDTO = new SKUQTYDTO();
        //                                qtyDTO.Id = item.Id;
        //                                qtyDTO.Type = (int)TriggerType.LossStock;
        //                                qtyDTO.ShopCode = orderDetailByLossEntity.STORECODE;
        //                                qtyDTO.SKUCode = orderDetailByLossEntity.GOODSCODE;
        //                                qtyDTO.Qty = orderDetailByLossEntity.ORIGINALQTY.Value;
        //                                qtyDTO.isSuccess = false;
        //                                qtyList.Add(qtyDTO);
        //                            }
        //                            else
        //                            {
        //                                foreach (var st in stListByMother)
        //                                {
        //                                    var qtyDTO = new SKUQTYDTO();
        //                                    qtyDTO.Id = item.Id;
        //                                    qtyDTO.Type = (int)TriggerType.LossStock;
        //                                    qtyDTO.ShopCode = orderDetailByLossEntity.STORECODE;
        //                                    qtyDTO.SKUCode = st.CHILDSKUCODE;
        //                                    qtyDTO.Qty = Math.Floor(orderDetailByLossEntity.ORIGINALQTY.Value / st.QTYPERMOTHER.Value);
        //                                    qtyDTO.isSuccess = false;
        //                                    qtyList.Add(qtyDTO);
        //                                }
        //                                foreach (var st in stListByChild)
        //                                {
        //                                    var qtyDTO = new SKUQTYDTO();
        //                                    qtyDTO.Id = item.Id;
        //                                    qtyDTO.Type = (int)TriggerType.LossStock;
        //                                    qtyDTO.ShopCode = orderDetailByLossEntity.STORECODE;
        //                                    qtyDTO.SKUCode = st.CHILDSKUCODE;
        //                                    qtyDTO.Qty = Math.Floor(orderDetailByLossEntity.ORIGINALQTY.Value / st.QTYPERMOTHER.Value);
        //                                    qtyDTO.isSuccess = false;
        //                                    qtyList.Add(qtyDTO);
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
        //                        }
        //                        break;
        //                    #endregion
        //                    #region 商品状态更新
        //                    case (int)TriggerType.SaleStatus:
        //                        long orgSkuIdBySaleStatus = 0;
        //                        if (long.TryParse(item.DataId, out orgSkuIdBySaleStatus))
        //                        {
        //                            var orgSkuEntity = organizeSKULogic.GetSingleById(orgSkuIdBySaleStatus);
        //                            if (orgSkuEntity != null &&
        //                                !string.IsNullOrEmpty(orgSkuEntity.ORGANIZE_CODE) &&
        //                                !string.IsNullOrEmpty(orgSkuEntity.SKU_CODE))
        //                            {
        //                                OrganizeSKUDTO skuDto = new OrganizeSKUDTO();
        //                                skuDto.triggerData = item;
        //                                skuDto.sku = orgSkuEntity;

        //                                statusList.Add(Mapper.Map<SKUStatusChangeDTO>(skuDto));
        //                            }
        //                            else
        //                            {
        //                                pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
        //                        }
        //                        break;
        //                    #endregion
        //                    #region 商品转换率库存更新
        //                    case (int)TriggerType.PerStock:
        //                        long skuTransformId = 0;
        //                        if (long.TryParse(item.DataId, out skuTransformId))
        //                        {
        //                            var skuTransformEntity = skuTransformLogic.GetSingleById(skuTransformId);
        //                            if (skuTransformEntity != null &&
        //                                !string.IsNullOrEmpty(skuTransformEntity.MOTHERSKUCODE) &&
        //                                !string.IsNullOrEmpty(skuTransformEntity.CHILDSKUCODE) &&
        //                                skuTransformEntity.QTYPERMOTHER.HasValue &&
        //                                skuTransformEntity.QTYPERMOTHER.Value > 0)
        //                            {
        //                                SKUQTYPERChangeDTO skuDto = new SKUQTYPERChangeDTO();
        //                                skuDto.Id = item.Id;
        //                                skuDto.SKUCode = skuTransformEntity.CHILDSKUCODE;
        //                                skuDto.QtyPerBefore = item.ChangeValue;
        //                                skuDto.QtyPerAfter = skuTransformEntity.QTYPERMOTHER.Value;
        //                                skuDto.isSuccess = false;

        //                                perList.Add(skuDto);
        //                            }
        //                            else
        //                            {
        //                                pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
        //                        }
        //                        break;
        //                        #endregion
        //                }
        //            }

        //            #region 更新线上系统的商品价格、库存、状态
        //            #region 价格
        //            if (priceList.Count > 0)
        //            {
        //                shopGoodsLogic.UpdateGoodsPrice(priceList);

        //                foreach (var item in priceList)
        //                {
        //                    if (item.isSuccess)
        //                    {
        //                        var result = pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
        //                        if (result)
        //                        {
        //                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " price update success");
        //                        }
        //                        else
        //                        {
        //                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " price update failure");
        //                        }
        //                    }
        //                }
        //            }
        //            #endregion
        //            #region 库存
        //            if (qtyList.Count > 0)
        //            {
        //                shopGoodsLogic.UpdateGoodsQty(qtyList);

        //                foreach (var item in qtyList)
        //                {
        //                    if (item.isSuccess)
        //                    {
        //                        var result = pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
        //                        if (result)
        //                        {
        //                            switch (item.Type)
        //                            {
        //                                case (int)TriggerType.InStock:
        //                                    Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " in stock update success");
        //                                    break;
        //                                case (int)TriggerType.SaleStock:
        //                                    Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " sale stock update success");
        //                                    break;
        //                                case (int)TriggerType.ReturnStock:
        //                                    Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " return stock update success");
        //                                    break;
        //                                case (int)TriggerType.LossStock:
        //                                    Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " loss stock update success");
        //                                    break;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            switch (item.Type)
        //                            {
        //                                case (int)TriggerType.InStock:
        //                                    Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " in stock update failure");
        //                                    break;
        //                                case (int)TriggerType.SaleStock:
        //                                    Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " sale stock update failure");
        //                                    break;
        //                                case (int)TriggerType.ReturnStock:
        //                                    Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " return stock update failure");
        //                                    break;
        //                                case (int)TriggerType.LossStock:
        //                                    Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " loss stock update failure");
        //                                    break;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            #endregion
        //            #region 状态
        //            if (statusList.Count > 0)
        //            {
        //                shopGoodsLogic.UpdateGoodsStatus(statusList);

        //                foreach (var item in statusList)
        //                {
        //                    if (item.isSuccess)
        //                    {
        //                        var result = pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
        //                        if (result)
        //                        {
        //                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " status update success");
        //                        }
        //                        else
        //                        {
        //                            Console.WriteLine("shop: " + item.ShopCode + ", goods: " + item.SKUCode + " status update failure");
        //                        }
        //                    }
        //                }
        //            }
        //            #endregion
        //            #region 转换率
        //            if (perList.Count > 0)
        //            {
        //                shopGoodsLogic.UpdateGoodsInventoryByQtyPer(perList);

        //                foreach (var item in perList)
        //                {
        //                    if (item.isSuccess)
        //                    {
        //                        var result = pp_TriggerDataLogic.UpdateStatus(item.Id, (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess);
        //                        if (result)
        //                        {
        //                            Console.WriteLine("goods: " + item.SKUCode + " qty per update inventory success");
        //                        }
        //                        else
        //                        {
        //                            Console.WriteLine("goods: " + item.SKUCode + " qty per update inventory failure");
        //                        }
        //                    }
        //                }
        //            }
        //            #endregion
        //            #endregion
        //        }
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("error: " + ex.ToString());
        //    }
        //}

        //public void Process2()
        //{
        //    while (true)
        //    {
        //        try
        //        {
        //            var priceList = new List<SKUPriceChangeDTO>();
        //            var stockList = new List<SKUStockChangeDTO>();
        //            using (OracleDbContext db = new OracleDbContext(oracleBuilder.Options))
        //            {
        //                var query = db.PP_TriggerDatas.Where(x => ((x.DataStatus & (int)TriggerDataStatus.Process) != (int)TriggerDataStatus.Process) ||
        //                                                          ((x.DataStatus & (int)TriggerDataStatus.Process) == (int)TriggerDataStatus.Process && (x.DataStatus & (int)TriggerDataStatus.ProcessSuccess) != (int)TriggerDataStatus.ProcessSuccess));
        //                if (query.Count() > 0)
        //                {
        //                    var list = query.OrderBy(x => x.CreateTime).ToList();
        //                    using (var scope = new TransactionScope())
        //                    {
        //                        foreach (var item in list)
        //                        {
        //                            switch (item.Type)
        //                            {
        //                                //商品价格更新
        //                                case (int)TriggerType.Price:
        //                                    long orgSkuId = 0;
        //                                    if (long.TryParse(item.DataId, out orgSkuId))
        //                                    {
        //                                        var orgSkuEntity = db.OrganizeSKUs.Where(x => x.ID == orgSkuId).FirstOrDefault();
        //                                        if (orgSkuEntity != null &&
        //                                            !string.IsNullOrEmpty(orgSkuEntity.ORGANIZE_CODE) &&
        //                                            !string.IsNullOrEmpty(orgSkuEntity.SKU_CODE) &&
        //                                            orgSkuEntity.SPRICE.HasValue)
        //                                        {
        //                                            OrganizeSKUDTO skuDto = new OrganizeSKUDTO();
        //                                            skuDto.triggerData = item;
        //                                            skuDto.sku = orgSkuEntity;

        //                                            priceList.Add(Mapper.Map<SKUPriceChangeDTO>(skuDto));
        //                                        }
        //                                        else
        //                                        {
        //                                            var entity = db.PP_TriggerDatas.Where(x => x.Id.Equals(item.Id)).FirstOrDefault();
        //                                            entity.DataStatus = (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess;
        //                                            db.Set<PP_TriggerData>().Attach(entity);
        //                                            db.Entry<PP_TriggerData>(entity).State = EntityState.Modified;
        //                                            var effect = db.SaveChanges();
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        var entity = db.PP_TriggerDatas.Where(x => x.Id.Equals(item.Id)).FirstOrDefault();
        //                                        entity.DataStatus = (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess;
        //                                        db.Set<PP_TriggerData>().Attach(entity);
        //                                        db.Entry<PP_TriggerData>(entity).State = EntityState.Modified;
        //                                        var effect = db.SaveChanges();
        //                                    }
        //                                    break;
        //                                //库存更新
        //                                case (int)TriggerType.InStock:
        //                                    var stockEntity = db.Stocks.Where(x => x.ID.Equals(item.DataId)).FirstOrDefault();
        //                                    if (stockEntity != null &&
        //                                        !string.IsNullOrEmpty(stockEntity.ORGCODE) &&
        //                                        !string.IsNullOrEmpty(stockEntity.GOODSCODE))
        //                                    {
        //                                        StockTransformDTO stDto = new StockTransformDTO();
        //                                        stDto.triggerData = item;

        //                                        //检查是否为标品,存在于转换表里就是非标品
        //                                        var skutfQuery = db.SKUTransforms.Where(x => x.MOTHERSKUID == stockEntity.GOODSID || x.CHILDSKUID == stockEntity.GOODSID);
        //                                        if (skutfQuery.Count() > 0)
        //                                        {
        //                                            var skutfList = skutfQuery.ToList();
        //                                            var motherSKUtfList = skutfList.Where(x => x.MOTHERSKUID == stockEntity.GOODSID).ToList();
        //                                            if (motherSKUtfList.Count == 0)
        //                                            {
        //                                                //当前库存发生变化的是子码
        //                                                var childSKUtfEntity = skutfList.Where(x => x.CHILDSKUID == stockEntity.GOODSID).FirstOrDefault();
        //                                                if (childSKUtfEntity == null)
        //                                                {
        //                                                    var entity = db.PP_TriggerDatas.Where(x => x.Id.Equals(item.Id)).FirstOrDefault();
        //                                                    entity.DataStatus = (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess;
        //                                                    db.Set<PP_TriggerData>().Attach(entity);
        //                                                    db.Entry<PP_TriggerData>(entity).State = EntityState.Modified;
        //                                                    var effect = db.SaveChanges();
        //                                                    continue;
        //                                                }
        //                                                //通过子码找到母码，获取母码的库存
        //                                                var motherSKUStockEntity = db.Stocks.Where(x => x.GOODSID == childSKUtfEntity.MOTHERSKUID).FirstOrDefault();
        //                                                if (motherSKUStockEntity == null)
        //                                                {
        //                                                    var entity = db.PP_TriggerDatas.Where(x => x.Id.Equals(item.Id)).FirstOrDefault();
        //                                                    entity.DataStatus = (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess;
        //                                                    db.Set<PP_TriggerData>().Attach(entity);
        //                                                    db.Entry<PP_TriggerData>(entity).State = EntityState.Modified;
        //                                                    var effect = db.SaveChanges();
        //                                                    continue;
        //                                                }

        //                                                stDto.childCodeStock = stockEntity;
        //                                                stDto.motherCodeStock = motherSKUStockEntity;
        //                                                stDto.skuTF = childSKUtfEntity;
        //                                                stDto.isMotherCode = false;

        //                                                //计算当前子码的库存=母码库存/转换率 + 子码库存
        //                                                stockList.Add(Mapper.Map<SKUStockChangeDTO>(stDto));
        //                                            }
        //                                            else
        //                                            {
        //                                                //当前库存发生变化的是母码
        //                                                //获取母码所有的子码，根据转换率计算每个子码的库存量
        //                                                foreach (var skuTF in motherSKUtfList)
        //                                                {
        //                                                    Stock childStock = new Stock();
        //                                                    childStock.GOODSCODE = skuTF.CHILDSKUCODE;
        //                                                    stDto.childCodeStock = childStock;
        //                                                    stDto.motherCodeStock = stockEntity;
        //                                                    stDto.skuTF = skuTF;
        //                                                    stDto.isMotherCode = true;

        //                                                    stockList.Add(Mapper.Map<SKUStockChangeDTO>(stDto));
        //                                                }
        //                                            }
        //                                        }
        //                                        else
        //                                        {
        //                                            stDto.childCodeStock = stockEntity;
        //                                            stDto.motherCodeStock = null;
        //                                            stDto.skuTF = null;
        //                                            stDto.isMotherCode = false;

        //                                            //标品
        //                                            stockList.Add(Mapper.Map<SKUStockChangeDTO>(stDto));
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        var entity = db.PP_TriggerDatas.Where(x => x.Id.Equals(item.Id)).FirstOrDefault();
        //                                        entity.DataStatus = (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess;
        //                                        db.Set<PP_TriggerData>().Attach(entity);
        //                                        db.Entry<PP_TriggerData>(entity).State = EntityState.Modified;
        //                                        var effect = db.SaveChanges();
        //                                    }
        //                                    break;
        //                            }
        //                        }
        //                        scope.Complete();
        //                    }
        //                }
        //            }

        //            #region 价格
        //            foreach (var item in priceList)
        //            {
        //                using (MsSqlDbContext db = new MsSqlDbContext(mssqlBuilder.Options))
        //                {
        //                    var query = db.PP_ShopGoodses.Where(x => x.isOfflineSKUPrice.HasValue && x.isOfflineSKUPrice.Value).Join(db.PP_Shops.Where(x => x.shopCode.Equals(item.ShopCode)), sg => sg.shopId, s => s.shopId, (sg, s) => sg);
        //                    var query1 = query.Join(db.PP_GoodsSKUs.Where(x => x.goodsCode.Equals(item.SKUCode)), q => q.skuId, gs => gs.skuId, (q, gs) => q);
        //                    var shopGoodsList = query1.ToList();
        //                    if (shopGoodsList.Count > 0)
        //                    {
        //                        var sgEntity = shopGoodsList.FirstOrDefault();
        //                        var price = Convert.ToDouble(item.Price);
        //                        if (!sgEntity.goodsPrice.HasValue || (price - sgEntity.goodsPrice.Value) != 0)
        //                        {
        //                            var effect = db.PP_ShopGoodses.Where(x => x.sgid == sgEntity.sgid).Update(x => new PP_ShopGoods { goodsPrice = price });
        //                            if (effect > 0)
        //                            {
        //                                item.isSuccess = true;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            item.isSuccess = true;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        item.isSuccess = true;
        //                    }
        //                }

        //                if (item.isSuccess)
        //                {
        //                    using (OracleDbContext db = new OracleDbContext(oracleBuilder.Options))
        //                    {
        //                        var entity = db.PP_TriggerDatas.Where(x => x.Id.Equals(item.Id)).FirstOrDefault();
        //                        entity.DataStatus = (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess;
        //                        db.Set<PP_TriggerData>().Attach(entity);
        //                        db.Entry<PP_TriggerData>(entity).State = EntityState.Modified;
        //                        var effect = db.SaveChanges();
        //                        if (effect > 0)
        //                        {
        //                            Console.WriteLine("门店: " + item.ShopCode + "的商品: " + item.SKUCode + "价格更新成功");
        //                        }
        //                        else
        //                        {
        //                            Console.WriteLine("门店: " + item.ShopCode + "的商品: " + item.SKUCode + "价格更新失败");
        //                        }
        //                    }
        //                }
        //            }
        //            #endregion

        //            #region 库存
        //            foreach (var item in stockList)
        //            {
        //                using (MsSqlDbContext db = new MsSqlDbContext(mssqlBuilder.Options))
        //                {
        //                    var query = db.PP_ShopGoodses.Where(x => x.isOfflineSKU.HasValue && x.isOfflineSKU.Value).Join(db.PP_Shops.Where(x => x.shopCode.Equals(item.ShopCode)), sg => sg.shopId, s => s.shopId, (sg, s) => sg);
        //                    var query1 = query.Join(db.PP_GoodsSKUs.Where(x => x.goodsCode.Equals(item.SKUCode)), q => q.skuId, gs => gs.skuId, (q, gs) => q);
        //                    var shopGoodsList = query1.ToList();
        //                    if (shopGoodsList.Count > 0)
        //                    {
        //                        var sgEntity = shopGoodsList.FirstOrDefault();
        //                        var qty = Convert.ToDouble(item.Qty);
        //                        if (!sgEntity.inventory.HasValue || (qty - sgEntity.inventory.Value) != 0)
        //                        {
        //                            using (var scope = new TransactionScope())
        //                            {
        //                                db.PP_ShopGoodses.Where(x => x.sgid == sgEntity.sgid).Update(x => new PP_ShopGoods { inventory = qty, status = 1 });

        //                                if (qty == 0)
        //                                {
        //                                    var goodsModel = db.PP_Goodses.Where(x => true).Join(db.PP_GoodsSKUs.Where(x => x.skuId == sgEntity.skuId), g => g.goodsCode, gs => gs.goodsCode, (g, gs) => g).FirstOrDefault();
        //                                    var shopModel = db.PP_Shops.Where(x => x.shopId == sgEntity.shopId).FirstOrDefault();
        //                                    PP_GoodsStockAlerm gsaEntity = new PP_GoodsStockAlerm();
        //                                    gsaEntity.goodsId = goodsModel == null ? 0 : goodsModel.goodsId;
        //                                    gsaEntity.goodsCode = goodsModel == null ? "" : goodsModel.goodsCode;
        //                                    gsaEntity.goodsName = goodsModel == null ? "" : goodsModel.goodsName;
        //                                    gsaEntity.shopId = shopModel == null ? 0 : shopModel.shopId;
        //                                    gsaEntity.shopCode = shopModel == null ? "" : shopModel.shopCode;
        //                                    gsaEntity.shopName = shopModel == null ? "" : shopModel.shopName;
        //                                    gsaEntity.createTime = DateTime.Now;

        //                                    db.Set<PP_GoodsStockAlerm>().Attach(gsaEntity);
        //                                    db.Entry<PP_GoodsStockAlerm>(gsaEntity).State = EntityState.Added;
        //                                    db.SaveChanges();
        //                                }
        //                                else
        //                                {
        //                                    var entityList = db.PP_GoodsStockAlerms.Where(x => x.shopCode.Equals(item.ShopCode) && x.goodsCode.Equals(item.SKUCode)).ToList();
        //                                    foreach (var entity in entityList)
        //                                    {
        //                                        db.Set<PP_GoodsStockAlerm>().Attach(entity);
        //                                        db.Entry<PP_GoodsStockAlerm>(entity).State = EntityState.Deleted;
        //                                        db.SaveChanges();
        //                                    }
        //                                }

        //                                scope.Complete();
        //                                item.isSuccess = true;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            item.isSuccess = true;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        item.isSuccess = true;
        //                    }
        //                }

        //                if (item.isSuccess)
        //                {
        //                    using (OracleDbContext db = new OracleDbContext(oracleBuilder.Options))
        //                    {
        //                        var entity = db.PP_TriggerDatas.Where(x => x.Id.Equals(item.Id)).FirstOrDefault();
        //                        entity.DataStatus = (int)TriggerDataStatus.Process + (int)TriggerDataStatus.ProcessSuccess;
        //                        db.Set<PP_TriggerData>().Attach(entity);
        //                        db.Entry<PP_TriggerData>(entity).State = EntityState.Modified;
        //                        var effect = db.SaveChanges();
        //                        if (effect > 0)
        //                        {
        //                            Console.WriteLine("门店: " + item.ShopCode + "的商品: " + item.SKUCode + "库存更新成功");
        //                        }
        //                        else
        //                        {
        //                            Console.WriteLine("门店: " + item.ShopCode + "的商品: " + item.SKUCode + "库存更新失败");
        //                        }
        //                    }
        //                }
        //            }
        //            #endregion
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("系统异常: " + ex.Message + "\n\r" + "DEBUG: " + ex.ToString());
        //            Console.WriteLine("按任意键退出");
        //            Console.ReadKey();
        //            break;
        //        }
        //    }
        //}
        #endregion
    }
}
