using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DbContext;
using Model.DTO.Oracle;
using Model.Entities.MsSql;

namespace Logic.MsSql
{
    public class PP_ShopGoodsLogic : BaseLogic
    {
        /// <summary>
        /// 更新商品价格
        /// </summary>
        /// <param name="list">商品价格异动列表</param>
        /// <returns></returns>
        public bool UpdateGoodsPrice(List<SKUPriceChangeDTO> list)
        {
            bool result = false;
            using (var scope = new TransactionScope())
            {
                foreach (var item in list)
                {
                    using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
                    {
                        var query = db.PP_ShopGoodses.Where(x => x.isOfflineSKUPrice.HasValue && x.isOfflineSKUPrice.Value).Join(db.PP_Shops.Where(x => x.shopCode.Equals(item.ShopCode)), sg => sg.shopId, s => s.shopId, (sg, s) => sg);
                        var query1 = query.Join(db.PP_GoodsSKUs.Where(x => x.goodsCode.Equals(item.SKUCode)), q => q.skuId, gs => gs.skuId, (q, gs) => q);
                        var shopGoodsList = query1.ToList();
                        if (shopGoodsList.Count > 0)
                        {
                            var sgEntity = shopGoodsList.FirstOrDefault();
                            var price = Convert.ToDouble(item.Price);
                            if (!sgEntity.goodsPrice.HasValue || (price - sgEntity.goodsPrice.Value) != 0)
                            {
                                sgEntity.goodsPrice = price;
                                db.Attach(sgEntity);
                                db.Entry(sgEntity).Property(p => p.goodsPrice).IsModified = true;
                                var effect = db.SaveChanges();
                                if (effect > 0)
                                {
                                    item.isSuccess = true;
                                }
                            }
                            else
                            {
                                item.isSuccess = true;
                            }
                        }
                        else
                        {
                            item.isSuccess = true;
                        }
                    }
                }
                scope.Complete();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 更新商品库存
        /// </summary>
        /// <param name="list">商品库存异动列表</param>
        /// <returns></returns>
        public bool UpdateGoodsInventory(List<SKUQTYChangeDTO> list)
        {
            bool result = false;
            using (var scope = new TransactionScope())
            {
                foreach (var item in list)
                {
                    using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
                    {
                        var query = db.PP_ShopGoodses.Where(x => x.isOfflineSKU.HasValue && x.isOfflineSKU.Value).Join(db.PP_Shops.Where(x => x.shopCode.Equals(item.ShopCode)), sg => sg.shopId, s => s.shopId, (sg, s) => sg);
                        var query1 = query.Join(db.PP_GoodsSKUs.Where(x => x.goodsCode.Equals(item.SKUCode)), q => q.skuId, gs => gs.skuId, (q, gs) => q);
                        var shopGoodsList = query1.ToList();
                        if (shopGoodsList.Count > 0)
                        {
                            var sgEntity = shopGoodsList.FirstOrDefault();
                            var qty = Convert.ToDouble(item.Qty);
                            if (!sgEntity.inventory.HasValue || (qty - sgEntity.inventory.Value) != 0)
                            {
                                if (qty <= 0)
                                {
                                    sgEntity.inventory = qty;
                                    sgEntity.status = 2;
                                    db.Attach(sgEntity);
                                    db.Entry(sgEntity).Property(p => p.inventory).IsModified = true;
                                    db.Entry(sgEntity).Property(p => p.status).IsModified = true;
                                    db.SaveChanges();

                                    var goodsModel = db.PP_Goodses.Where(x => true).Join(db.PP_GoodsSKUs.Where(x => x.skuId == sgEntity.skuId), g => g.goodsCode, gs => gs.goodsCode, (g, gs) => g).FirstOrDefault();
                                    var shopModel = db.PP_Shops.Where(x => x.shopId == sgEntity.shopId).FirstOrDefault();
                                    PP_GoodsStockAlerm gsaEntity = new PP_GoodsStockAlerm();
                                    gsaEntity.goodsId = goodsModel == null ? 0 : goodsModel.goodsId;
                                    gsaEntity.goodsCode = goodsModel == null ? "" : goodsModel.goodsCode;
                                    gsaEntity.goodsName = goodsModel == null ? "" : goodsModel.goodsName;
                                    gsaEntity.shopId = shopModel == null ? 0 : shopModel.shopId;
                                    gsaEntity.shopCode = shopModel == null ? "" : shopModel.shopCode;
                                    gsaEntity.shopName = shopModel == null ? "" : shopModel.shopName;
                                    gsaEntity.createTime = DateTime.Now;

                                    db.Set<PP_GoodsStockAlerm>().Attach(gsaEntity);
                                    db.Entry<PP_GoodsStockAlerm>(gsaEntity).State = EntityState.Added;
                                    db.SaveChanges();

                                    item.isSuccess = true;
                                }
                                else
                                {
                                    sgEntity.inventory = qty;
                                    sgEntity.status = 1;
                                    db.Attach(sgEntity);
                                    db.Entry(sgEntity).Property(p => p.inventory).IsModified = true;
                                    db.Entry(sgEntity).Property(p => p.status).IsModified = true;
                                    db.SaveChanges();

                                    var entityList = db.PP_GoodsStockAlerms.Where(x => x.shopCode.Equals(item.ShopCode) && x.goodsCode.Equals(item.SKUCode)).ToList();
                                    foreach (var entity in entityList)
                                    {
                                        db.Set<PP_GoodsStockAlerm>().Attach(entity);
                                        db.Entry<PP_GoodsStockAlerm>(entity).State = EntityState.Deleted;
                                        db.SaveChanges();
                                    }
                                    item.isSuccess = true;
                                }
                            }
                            else
                            {
                                item.isSuccess = true;
                            }
                        }
                        else
                        {
                            item.isSuccess = true;
                        }
                    }
                }
                scope.Complete();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 根据数量变换更新商品库存
        /// </summary>
        /// <param name="list">商品数量列表</param>
        /// <returns></returns>
        public bool UpdateGoodsQty(List<SKUQTYChangeDTO> list)
        {
            bool result = false;
            using (var scope = new TransactionScope())
            {
                foreach (var item in list)
                {
                    using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
                    {
                        var query = db.PP_ShopGoodses.Where(x => x.isOfflineSKU.HasValue && x.isOfflineSKU.Value).Join(db.PP_Shops.Where(x => x.shopCode.Equals(item.ShopCode)), sg => sg.shopId, s => s.shopId, (sg, s) => sg);
                        var query1 = query.Join(db.PP_GoodsSKUs.Where(x => x.goodsCode.Equals(item.SKUCode)), q => q.skuId, gs => gs.skuId, (q, gs) => q);
                        var shopGoodsList = query1.ToList();
                        if (shopGoodsList.Count > 0)
                        {
                            var sgEntity = shopGoodsList.FirstOrDefault();
                            double qty = sgEntity.inventory.HasValue ? sgEntity.inventory.Value : 0;
                            switch (item.Type)
                            {
                                case (int)TriggerType.DistributeInStock:
                                    qty = qty + Convert.ToDouble(item.Qty);
                                    item.isSuccess = true;
                                    break;
                                case (int)TriggerType.AllotInStock:
                                    qty = qty + Convert.ToDouble(item.Qty);
                                    break;
                                case (int)TriggerType.DeliveryInStock:
                                    qty = qty + Convert.ToDouble(item.Qty);
                                    break;
                                case (int)TriggerType.POSCancelInStock:
                                    qty = qty + Convert.ToDouble(item.Qty);
                                    break;
                                case (int)TriggerType.POSSaleOutStock:
                                    qty = qty - Convert.ToDouble(item.Qty);
                                    break;
                                case (int)TriggerType.LossOutStock:
                                    qty = qty - Convert.ToDouble(item.Qty);
                                    break;
                                case (int)TriggerType.AllotOutStock:
                                    qty = qty - Convert.ToDouble(item.Qty);
                                    break;
                                case (int)TriggerType.DeliveryCancelOutStock:
                                    qty = qty - Convert.ToDouble(item.Qty);
                                    break;
                            }
                            if (qty <= 0)
                            {
                                sgEntity.inventory = 0;
                                sgEntity.status = 2;
                                db.Attach(sgEntity);
                                db.Entry(sgEntity).Property(p => p.inventory).IsModified = true;
                                db.Entry(sgEntity).Property(p => p.status).IsModified = true;
                                db.SaveChanges();

                                var goodsModel = db.PP_Goodses.Where(x => true).Join(db.PP_GoodsSKUs.Where(x => x.skuId == sgEntity.skuId), g => g.goodsCode, gs => gs.goodsCode, (g, gs) => g).FirstOrDefault();
                                var shopModel = db.PP_Shops.Where(x => x.shopId == sgEntity.shopId).FirstOrDefault();
                                PP_GoodsStockAlerm gsaEntity = new PP_GoodsStockAlerm();
                                gsaEntity.goodsId = goodsModel == null ? 0 : goodsModel.goodsId;
                                gsaEntity.goodsCode = goodsModel == null ? "" : goodsModel.goodsCode;
                                gsaEntity.goodsName = goodsModel == null ? "" : goodsModel.goodsName;
                                gsaEntity.shopId = shopModel == null ? 0 : shopModel.shopId;
                                gsaEntity.shopCode = shopModel == null ? "" : shopModel.shopCode;
                                gsaEntity.shopName = shopModel == null ? "" : shopModel.shopName;
                                gsaEntity.createTime = DateTime.Now;

                                db.Set<PP_GoodsStockAlerm>().Attach(gsaEntity);
                                db.Entry<PP_GoodsStockAlerm>(gsaEntity).State = EntityState.Added;
                                db.SaveChanges();

                                item.isSuccess = true;
                            }
                            else
                            {
                                sgEntity.inventory = qty;
                                sgEntity.status = 1;
                                db.Attach(sgEntity);
                                db.Entry(sgEntity).Property(p => p.inventory).IsModified = true;
                                db.Entry(sgEntity).Property(p => p.status).IsModified = true;
                                db.SaveChanges();

                                var entityList = db.PP_GoodsStockAlerms.Where(x => x.shopCode.Equals(item.ShopCode) && x.goodsCode.Equals(item.SKUCode)).ToList();
                                foreach (var entity in entityList)
                                {
                                    db.Set<PP_GoodsStockAlerm>().Attach(entity);
                                    db.Entry<PP_GoodsStockAlerm>(entity).State = EntityState.Deleted;
                                    db.SaveChanges();
                                }

                                item.isSuccess = true;
                            }
                        }
                        else
                        {
                            item.isSuccess = true;
                        }
                    }
                }
                scope.Complete();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 更新商品状态
        /// </summary>
        /// <param name="list">商品状态异动列表</param>
        /// <returns></returns>
        public bool UpdateGoodsStatus(List<SKUStatusChangeDTO> list)
        {
            bool result = false;
            using (var scope = new TransactionScope())
            {
                foreach (var item in list)
                {
                    using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
                    {
                        var query = db.PP_ShopGoodses.Where(x => true).Join(db.PP_GoodsSKUs.Where(x => x.goodsCode.Equals(item.SKUCode)), q => q.skuId, gs => gs.skuId, (q, gs) => q);
                        var shopGoodsList = query.ToList();
                        if (shopGoodsList.Count > 0)
                        {
                            foreach (var shopGoods in shopGoodsList)
                            {
                                var sgEntity = shopGoodsList.FirstOrDefault();
                                sgEntity.status = item.Status;
                                db.Attach(sgEntity);
                                db.Entry(sgEntity).Property(p => p.status).IsModified = true;
                                db.SaveChanges();
                            }
                            item.isSuccess = true;
                        }
                        else
                        {
                            item.isSuccess = true;
                        }
                    }
                }
                scope.Complete();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 根据商品转化率更新库存
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UpdateGoodsInventoryByQtyPer(List<SKUQTYPERChangeDTO> list)
        {
            bool result = false;
            using (var scope = new TransactionScope())
            {
                foreach (var item in list)
                {
                    using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
                    {
                        var query = db.PP_ShopGoodses.Where(x => true).Join(db.PP_GoodsSKUs.Where(x => x.goodsCode.Equals(item.SKUCode)), sg => sg.skuId, gs => gs.skuId, (sg, gs) => sg);
                        var shopGoodsList = query.ToList();
                        foreach (var shopGoods in shopGoodsList)
                        {
                            var inventory = (shopGoods.inventory.HasValue ? shopGoods.inventory.Value : 0) * Convert.ToDouble(item.QtyPerBefore / item.QtyPerAfter);
                            shopGoods.inventory = inventory;
                            db.Attach(shopGoods);
                            db.Entry(shopGoods).Property(p => p.inventory).IsModified = true;
                            db.SaveChanges();
                            item.isSuccess = true;
                        }
                    }
                }
                scope.Complete();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 根据商品转化率绑定更新库存
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UpdateGoodsInventoryByTransform(List<SKUQTYPERChangeDTO> list)
        {
            bool result = false;
            using (var scope = new TransactionScope())
            {
                foreach (var item in list)
                {
                    using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
                    {
                        var query = db.PP_ShopGoodses.Where(x => true).Join(db.PP_GoodsSKUs.Where(x => x.goodsCode.Equals(item.SKUCode)), sg => sg.skuId, gs => gs.skuId, (sg, gs) => sg);
                        var shopGoodsList = query.ToList();
                        foreach (var shopGoods in shopGoodsList)
                        {
                            var inventory = (shopGoods.inventory.HasValue ? shopGoods.inventory.Value : 0) / Convert.ToDouble(item.QtyPerAfter);
                            shopGoods.inventory = inventory;
                            db.Attach(shopGoods);
                            db.Entry(shopGoods).Property(p => p.inventory).IsModified = true;
                            db.SaveChanges();
                            item.isSuccess = true;
                        }
                    }
                }
                scope.Complete();
                result = true;
            }
            return result;
        }
    }
}