using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Model.DbContext;
using Model.Entities.Oracle;

namespace Logic.Oracle
{
    public class StockBakLogic : BaseLogic
    {
        public bool Insert(StockBak entity)
        {
            bool result = false;
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                db.StockBak.Add(entity);
                db.SaveChanges();
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 获取门店的商品库存信息
        /// </summary>
        /// <param name="orgCode">门店CODE，为空则表示获取所有门店</param>
        /// <returns></returns>
        public List<StockBak> GetList(string orgCode)
        {
            List<StockBak> result = new List<StockBak>();

            Expression<Func<StockBak, bool>> expr = x => true;
            if (!string.IsNullOrEmpty(orgCode))
            {
                expr = expr.And(x => x.ORGCODE.Equals(orgCode));
            }

            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                var query = db.StockBak.Where(expr);
                foreach (var item in query)
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
