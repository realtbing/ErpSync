using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Model;
using Model.DbContext;
using Model.DTO.Oracle;
using Model.Entities.Oracle;

namespace Logic.Oracle
{
    public class ApplyOrderDetailLogic : BaseLogic
    {
        /// <summary>
        /// 获取门店的商品的申请信息
        /// </summary>
        /// <param name="orgCode">门店CODE，为空则表示获取所有门店</param>
        /// <param name="skuCode">商品CODE，为空则表示获取所有商品</param>
        /// <param name="orderType">订单类型(0:所有;1:退货;2:报损;3:差异反馈;4:调拨;5:返配;6:补偿;7:销售退货;8:报溢;9:上架;10:商品上下限)</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public List<ApplyOrderDetailDTO> GetList(string orgCode, string skuCode, int orderType, DateTime startTime, DateTime endTime)
        {
            List<ApplyOrderDetailDTO> result = new List<ApplyOrderDetailDTO>();

            Expression<Func<ApplyOrder, bool>> expr = x => x.AUDITON >= startTime && x.AUDITON <= endTime;
            if (!string.IsNullOrEmpty(orgCode))
            {
                expr = expr.And(x => x.ORGCODE.Equals(orgCode));
            }
            if (orderType > 0)
            {
                expr = expr.And(x => x.ORDERTYPE == orderType);
            }

            Expression<Func<ApplyOrderDetail, bool>> exprByDetail = x => true;
            if (!string.IsNullOrEmpty(skuCode))
            {
                exprByDetail = exprByDetail.And(x => x.GOODSCODE.Equals(skuCode));
            }

            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                var query = db.ApplyOrder.Where(expr).Join(db.ApplyOrderDetail.Where(exprByDetail), o => o.ID, od => od.ORDERID, (o, od) => new ApplyOrderDetailDTO
                {
                    ID = od.ID,
                    ORDERID = od.ORDERID,
                    ORDERCODE = od.ORDERCODE,
                    ORDERTYPE = o.ORDERTYPE,
                    ORGCODE = od.ORGCODE,
                    GOODSCODE = od.GOODSCODE,
                    PLANQTY = od.PLANQTY,
                    AUDITQTY = od.AUDITQTY,
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
        public ApplyOrderDetail GetSingleById(long Id)
        {
            ApplyOrderDetail result = null;
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.ApplyOrderDetail.Where(x => x.ID == Id).FirstOrDefault();
            }
            return result;
        }
    }
}
