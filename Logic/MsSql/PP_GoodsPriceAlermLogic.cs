using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Model.DbContext;
using Model.DTO.Oracle;
using Model.Entities.MsSql;

namespace Logic.MsSql
{
    public class PP_GoodsPriceAlermLogic : BaseLogic
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Add(List<SKUPriceChangeDTO> list)
        {
            bool result = false;
            using (var scope = new TransactionScope())
            {
                foreach (var item in list)
                {
                    using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
                    {
                        var goodsModel = db.PP_Goodses.Where(x => x.goodsCode.Equals(item.SKUCode)).FirstOrDefault();
                        var shopModel = db.PP_Shops.Where(x => x.shopCode.Equals(item.ShopCode)).FirstOrDefault();
                        var query = db.PP_ShopGoodses.Where(x => true).Join(db.PP_GoodsSKUs.Where(x => x.goodsCode.Equals(item.SKUCode)), sg => sg.skuId, gs => gs.skuId, (sg, gs) => sg);
                        var query1 = query.Join(db.PP_Shops.Where(x => x.shopCode.Equals(item.ShopCode)), q => q.shopId, s => s.shopId, (q, s) => q);
                        var shopGoodsList = query1.ToList();
                        if (shopGoodsList.Count > 0 && goodsModel != null && shopModel != null)
                        {
                            var sgModel = shopGoodsList.FirstOrDefault();
                            PP_GoodsPriceAlerm entity = new PP_GoodsPriceAlerm();
                            entity.goodsId = goodsModel.goodsId;
                            entity.goodsCode = goodsModel.goodsCode;
                            entity.goodsName = goodsModel.goodsName;
                            entity.shopId = shopModel.shopId;
                            entity.shopCode = shopModel.shopCode;
                            entity.shopName = shopModel.shopName;
                            entity.originPrice = Convert.ToDecimal(sgModel.goodsPrice.HasValue ? sgModel.goodsPrice.Value : 0);
                            entity.newPrice = item.Price;
                            entity.createTime = DateTime.Now;

                            db.Set<PP_GoodsPriceAlerm>().Attach(entity);
                            db.Entry<PP_GoodsPriceAlerm>(entity).State = EntityState.Added;
                            db.SaveChanges();
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
