using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Model.DbContext;
using Model.Entities.Oracle;

namespace Logic.Oracle
{
    public class StockSnapshotLogic : BaseLogic
    {
        /// <summary>
        /// 获取门店的日结信息，根据商品分组
        /// </summary>
        /// <param name="orgCode">门店CODE，为空则表示获取所有门店</param>
        /// <returns></returns>
        public List<StockSnapshot> GetLastStockSnapshot(string orgCode)
        {
            List<StockSnapshot> result = new List<StockSnapshot>();

            Expression<Func<StockSnapshot, bool>> expr = x => x.STATUS == 1;
            if (!string.IsNullOrEmpty(orgCode))
            {
                expr = expr.And(x => x.ORGCODE.Equals(orgCode));
            }

            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                var query = db.StockSnapshot.Where(expr).GroupBy(x => x.GOODSCODE);
                foreach (var item in query)
                {
                    result.Add(item.OrderByDescending(x => x.BACKUPDATE).FirstOrDefault());
                }
            }

            return result;
        }

        /// <summary>
        /// 获取门店商品的日结信息
        /// </summary>
        /// <param name="orgCode">门店CODE</param>
        /// <param name="skuCode">商品CODE</param>
        /// <returns></returns>
        public StockSnapshot GetLastStockSnapshot(string orgCode, string skuCode)
        {
            StockSnapshot result = null;

            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.StockSnapshot.Where(x => x.ORGCODE.Equals(orgCode) && x.GOODSCODE.Equals(skuCode)).OrderByDescending(x => x.BACKUPDATE).FirstOrDefault();
            }

            return result;
        }
    }
}
