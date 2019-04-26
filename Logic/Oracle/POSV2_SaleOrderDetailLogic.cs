using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Model;
using Model.DbContext;
using Model.DTO.Oracle;
using Model.Entities.Oracle;

namespace Logic.Oracle
{
    public class POSV2_SaleOrderDetailLogic : BaseLogic
    {
        /// <summary>
        /// 获取门店的商品库存信息
        /// </summary>
        /// <param name="orgCode">门店CODE，为空则表示获取所有门店</param>
        /// <param name="skuCode">商品CODE，为空则表示获取所有商品</param>
        /// <param name="orderType">订单类型(0:所有;1:销售;2:退货;5:报损)</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public List<POSV2_SaleOrderDetailDTO> GetList(string orgCode, string skuCode, int orderType, DateTime startTime, DateTime endTime)
        {
            List<POSV2_SaleOrderDetailDTO> result = new List<POSV2_SaleOrderDetailDTO>();

            Expression<Func<POSV2_SaleOrder, bool>> expr = x => x.CREATEON >= startTime && x.CREATEON <= endTime;
            if (!string.IsNullOrEmpty(orgCode))
            {
                expr = expr.And(x => x.STORECODE.Equals(orgCode));
            }
            if (orderType > 0)
            {
                expr = expr.And(x => x.SALEORDERTYPE == orderType);
            }

            Expression<Func<POSV2_SaleOrderDetail, bool>> exprByDetail = x => true;
            if (!string.IsNullOrEmpty(skuCode))
            {
                exprByDetail = exprByDetail.And(x => x.GOODSCODE.Equals(skuCode));
            }

            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                var query = db.POSV2_SaleOrders.Where(expr).Join(db.POSV2_SaleOrderDetails.Where(exprByDetail), o => o.ID, od => od.ORDERID, (o, od) => new POSV2_SaleOrderDetailDTO
                {
                    ID = od.ID,
                    STORECODE = od.STORECODE,
                    STORENAME = od.STORENAME,
                    POSCODE = od.POSCODE,
                    POSNAME = od.POSNAME,
                    ORDERID = od.ORDERID,
                    ORDERCODE = od.ORDERCODE,
                    ORDERTYPE = o.SALEORDERTYPE,
                    ORIGINALTAREQTY = od.ORIGINALTAREQTY,
                    ORIGINALQTY = od.ORIGINALQTY,
                    QTY = od.QTY
                });
                result = query.ToList();
            }

            return result;
        }

        /// <summary>
        /// 获取门店商品的销售、退货、报损统计
        /// </summary>
        /// <param name="orgCode">门店CODE，为空则表示获取所有门店</param>
        /// <param name="skuCode">商品CODE，为空则表示获取所有商品</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public Dictionary<int, decimal> GetStatisticsByOrgAndSKU(string orgCode, string skuCode, DateTime startTime, DateTime endTime)
        {
            var list = GetList(orgCode, skuCode, 0, startTime, endTime);
            Dictionary<int, decimal> result = new Dictionary<int, decimal>();

            var saleNum = list.Where(x => x.ORDERTYPE == (int)POSV2_SaleOrderType.Sale).Sum(x => x.QTY);
            result.Add((int)POSV2_SaleOrderType.Sale, saleNum.HasValue ? saleNum.Value : 0);
            var returnNum = list.Where(x => x.ORDERTYPE == (int)POSV2_SaleOrderType.Cancel).Sum(x => x.QTY);
            result.Add((int)POSV2_SaleOrderType.Cancel, returnNum.HasValue ? returnNum.Value : 0);

            return result;
        }

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public POSV2_SaleOrderDetail GetSingleById(string Id)
        {
            POSV2_SaleOrderDetail result = null;
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.POSV2_SaleOrderDetails.Where(x => x.ID.Equals(Id)).FirstOrDefault();
            }
            return result;
        }
    }
}
