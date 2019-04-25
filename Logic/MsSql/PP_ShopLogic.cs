using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Model.DbContext;
using Model.Entities.MsSql;

namespace Logic.MsSql
{
    public class PP_ShopLogic : BaseLogic
    {
        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="status">状态(0:所有;1:营业;2:停业;)</param>
        /// <returns></returns>
        public List<PP_Shop> GetList(int status)
        {
            List<PP_Shop> result = new List<PP_Shop>();
            Expression<Func<PP_Shop, bool>> expr = x => true;
            if (status > 0)
            {
                expr = expr.And(x => x.status == status);
            }

            using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
            {
                result = db.PP_Shops.Where(expr).ToList();
            }
            return result;
        }
    }
}
