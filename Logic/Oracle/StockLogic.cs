using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Model.DbContext;
using Model.Entities.Oracle;

namespace Logic.Oracle
{
    public class StockLogic : BaseLogic
    {
        /// <summary>
        /// 获取门店的商品库存信息
        /// </summary>
        /// <param name="orgCode">门店CODE，为空则表示获取所有门店</param>
        /// <returns></returns>
        public List<Stock> GetList(string orgCode)
        {
            List<Stock> result = new List<Stock>();

            Expression<Func<Stock, bool>> expr = x => true;
            if (!string.IsNullOrEmpty(orgCode))
            {
                expr = expr.And(x => x.ORGCODE.Equals(orgCode));
            }

            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                var query = db.Stocks.Where(expr);
                foreach (var item in query)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取门店商品的库存信息
        /// </summary>
        /// <param name="orgCode">门店CODE</param>
        /// <param name="skuCode">商品CODE</param>
        /// <returns></returns>
        public Stock GetSingleByShopAndSKU(string orgCode, string skuCode)
        {
            Stock result = null;

            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.Stocks.Where(x => x.ORGCODE.Equals(orgCode) && x.GOODSCODE.Equals(skuCode)).FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Stock GetSingleById(string Id)
        {
            Stock result = null;
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.Stocks.Where(x => x.ID.Equals(Id)).FirstOrDefault();
            }
            return result;
        }
    }
}
