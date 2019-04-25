using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Logic;
using Model.DbContext;
using Model.DTO.Oracle;
using Model.Entities.Oracle;

namespace Logic.Oracle
{
    public class DeliveryOrderDetailLogic : BaseLogic
    {
        /// <summary>
        /// 获取门店商品的交货信息
        /// </summary>
        /// <param name="orgCode">门店CODE，为空则表示获取对象门店</param>
        /// <param name="refOrgCode">门店CODE，为空则表示获取所有引用门店</param>
        /// <param name="skuCode">商品CODE，为空则表示获取所有商品</param>
        /// <param name="orderType">订单类型(0:所有;1:订单;2:送货;3:配送;4:手工;5:返配;6:调拨;7:退货)</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public List<DeliveryOrderDetailDTO> GetList(string orgCode, string refOrgCode, string skuCode, int orderType, DateTime startTime, DateTime endTime)
        {
            List<DeliveryOrderDetailDTO> result = new List<DeliveryOrderDetailDTO>();

            Expression<Func<DeliveryOrder, bool>> expr = x => x.CREATEON >= startTime && x.CREATEON <= endTime;
            if (!string.IsNullOrEmpty(orgCode))
            {
                expr = expr.And(x => x.ORGCODE.Equals(orgCode));
            }
            if (!string.IsNullOrEmpty(refOrgCode))
            {
                expr = expr.And(x => x.REFORGCODE.Equals(refOrgCode));
            }
            if (orderType > 0)
            {
                expr = expr.And(x => x.DELIVERYORDERTYPE == orderType);
            }

            Expression<Func<DeliveryOrderDetail, bool>> exprByDetail = x => true;
            if (!string.IsNullOrEmpty(skuCode))
            {
                exprByDetail = exprByDetail.And(x => x.GOODSCODE.Equals(skuCode));
            }

            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                var query = db.DeliveryOrder.Where(expr).Join(db.DeliveryOrderDetail.Where(exprByDetail), o => o.ID, od => od.ORDERID, (o, od) => new DeliveryOrderDetailDTO
                {
                    ID = od.ID,
                    ORDERID = od.ORDERID,
                    ORDERCODE = od.ORDERCODE,
                    DELIVERYORDERTYPE = o.DELIVERYORDERTYPE,
                    ORGID = o.ORGID,
                    ORGCODE = o.ORGCODE,
                    REFORGID = o.REFORGID,
                    REFORGCODE = o.REFORGCODE,
                    GOODSID = od.GOODSID,
                    GOODSCODE = od.GOODSCODE,
                    PLANQTY = od.PLANQTY,
                    FACTQTY = od.FACTQTY
                });
                result = query.ToList();
            }

            return result;
        }

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DeliveryOrderDetailDTO GetSingleById(long Id)
        {
            DeliveryOrderDetailDTO result = null;
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.DeliveryOrder.Where(x => true).Join(db.DeliveryOrderDetail.Where(x => x.ID == Id), o => o.ID, od => od.ORDERID, (o, od) => new DeliveryOrderDetailDTO
                {
                    ID = od.ID,
                    ORDERID = od.ORDERID,
                    ORDERCODE = od.ORDERCODE,
                    DELIVERYORDERTYPE = o.DELIVERYORDERTYPE,
                    ORGID = o.ORGID,
                    ORGCODE = o.ORGCODE,
                    REFORGID = o.REFORGID,
                    REFORGCODE = o.REFORGCODE,
                    GOODSID = od.GOODSID,
                    GOODSCODE = od.GOODSCODE,
                    PLANQTY = od.PLANQTY,
                    FACTQTY = od.FACTQTY
                }).FirstOrDefault();
            }
            return result;
        }
    }
}
