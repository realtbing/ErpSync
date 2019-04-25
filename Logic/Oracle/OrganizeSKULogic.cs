using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Model.DbContext;
using Model.DTO.Oracle;
using Model.Entities.Oracle;

namespace Logic.Oracle
{
    public class OrganizeSKULogic : BaseLogic
    {
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="orgCode">门店CODE，为空则表示获取所有门店</param>
        /// <param name="skuCode">商品CODE，为空则表示获取所有商品</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="pageIndex">当前页码，从0开始，默认为0</param>
        /// <param name="pageSize">每页数量，默认为10</param>
        /// <returns></returns>
        public List<SKUPriceChangeDTO> GetPagerList(string orgCode, string skuCode, out int totalCount, int pageIndex = 0, int pageSize = 10)
        {
            List<SKUPriceChangeDTO> skuList = new List<SKUPriceChangeDTO>();

            Expression<Func<OrganizeSKU, bool>> expr = x => true;
            if (!string.IsNullOrEmpty(orgCode))
            {
                expr = expr.And(x => x.ORGANIZE_CODE.Equals(orgCode));
            }
            if (!string.IsNullOrEmpty(skuCode))
            {
                expr = expr.And(x => x.SKU_CODE.Equals(skuCode));
            }

            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                var query = db.OrganizeSKU.Where(expr);
                totalCount = query.Count();
                var list = query.OrderByDescending(x => x.ID).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                skuList = Mapper.Map<List<SKUPriceChangeDTO>>(list);
            }

            return skuList;
        }

        public OrganizeSKU GetSingleById(long Id)
        {
            OrganizeSKU result = null;
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.OrganizeSKU.Where(x => x.ID == Id).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="orgCode">门店CODE</param>
        /// <param name="skuCode">商品CODE</param>
        /// <returns></returns>
        public OrganizeSKU GetSingleByShopAndSKU(string orgCode, string skuCode)
        {
            OrganizeSKU result = null;
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.OrganizeSKU.Where(x => x.ORGANIZE_CODE.Equals(orgCode) && x.SKU_CODE.Equals(skuCode)).FirstOrDefault();
            }
            return result;
        }
    }
}
