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
    public class StockOrderDetailLogic : BaseLogic
    {
        /// <summary>
        /// 获取门店商品的交货信息
        /// </summary>
        /// <param name="orgCode">门店CODE，为空则表示获取对象门店</param>
        /// <param name="skuCode">商品CODE，为空则表示获取所有商品</param>
        /// <param name="orderType">订单类型(0:所有;16:配送入库;5:调拨入库;3:送货入库;13:报损出库;6:调拨出库;4:退货出库)</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public List<StockOrderDetailDTO> GetList(string orgCode, string skuCode, int orderType, DateTime startTime, DateTime endTime)
        {
            List<StockOrderDetailDTO> result = new List<StockOrderDetailDTO>();

            Expression<Func<StockOrder, bool>> expr = x => x.CREATEON >= startTime && x.CREATEON <= endTime;
            if (!string.IsNullOrEmpty(orgCode))
            {
                expr = expr.And(x => x.ORGCODE.Equals(orgCode));
            }
            if (orderType > 0)
            {
                expr = expr.And(x => x.ORDERTYPE == orderType);
            }

            Expression<Func<StockOrderDetail, bool>> exprByDetail = x => true;
            if (!string.IsNullOrEmpty(skuCode))
            {
                exprByDetail = exprByDetail.And(x => x.GOODSCODE.Equals(skuCode));
            }

            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                var query = db.StockOrder.Where(expr).Join(db.StockOrderDetail.Where(exprByDetail), o => o.ID, od => od.ORDERID, (o, od) => new StockOrderDetailDTO
                {
                    ID = od.ID,
                    ORDERID = od.ORDERID,
                    ORDERCODE = od.ORDERCODE,
                    ORDERTYPE = o.ORDERTYPE,
                    ORGID = o.ORGID,
                    ORGCODE = o.ORGCODE,
                    REFORGID = o.REFORGID,
                    REFORGCODE = o.REFORGCODE,
                    GOODSID = od.GOODSID,
                    GOODSCODE = od.GOODSCODE,
                    QTY = od.QTY
                });
                result = query.ToList();
            }

            return result;
        }

        /// <summary>
        /// 获取门店商品的配送入库、调拨入库、送货入库、报损出库、调拨出库、退货出库统计
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

            var num16 = list.Where(x => x.ORDERTYPE == (int)StockOrderType.DistributeIn).Sum(x => x.QTY);
            result.Add((int)StockOrderType.DistributeIn, num16.HasValue ? num16.Value : 0);
            var num5 = list.Where(x => x.ORDERTYPE == (int)StockOrderType.AllotIn).Sum(x => x.QTY);
            result.Add((int)StockOrderType.AllotIn, num5.HasValue ? num5.Value : 0);
            var num3 = list.Where(x => x.ORDERTYPE == (int)StockOrderType.Delivery).Sum(x => x.QTY);
            result.Add((int)StockOrderType.Delivery, num3.HasValue ? num3.Value : 0);
            var num13 = list.Where(x => x.ORDERTYPE == (int)StockOrderType.ReportLoss).Sum(x => x.QTY);
            result.Add((int)StockOrderType.ReportLoss, num13.HasValue ? num13.Value : 0);
            var num6 = list.Where(x => x.ORDERTYPE == (int)StockOrderType.AllotOut).Sum(x => x.QTY);
            result.Add((int)StockOrderType.AllotOut, num6.HasValue ? num6.Value : 0);
            var num4 = list.Where(x => x.ORDERTYPE == (int)StockOrderType.DeliveryCancel).Sum(x => x.QTY);
            result.Add((int)StockOrderType.DeliveryCancel, num4.HasValue ? num4.Value : 0);

            return result;
        }

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public StockOrderDetailDTO GetSingleById(string Id)
        {
            StockOrderDetailDTO result = null;
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.StockOrder.Where(x => true).Join(db.StockOrderDetail.Where(x => x.ID.Equals(Id)), o => o.ID, od => od.ORDERID, (o, od) => new StockOrderDetailDTO
                {
                    ID = od.ID,
                    ORDERID = od.ORDERID,
                    ORDERCODE = od.ORDERCODE,
                    ORDERTYPE = o.ORDERTYPE,
                    ORGID = o.ORGID,
                    ORGCODE = o.ORGCODE,
                    REFORGID = o.REFORGID,
                    REFORGCODE = o.REFORGCODE,
                    GOODSID = od.GOODSID,
                    GOODSCODE = od.GOODSCODE,
                    QTY = od.QTY
                }).FirstOrDefault();
            }
            return result;
        }
    }
}
