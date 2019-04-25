using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DbContext;
using Model.Entities.Oracle;

namespace Logic.Oracle
{
    public class PP_TriggerDataBakLoigc : BaseLogic
    {
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="isProcess">是否处理</param>
        /// <param name="isProcessSuccess">处理是否成功</param>
        /// <returns></returns>
        public List<PP_TriggerDataBak> GetList(bool isProcess, bool isProcessSuccess)
        {
            List<PP_TriggerDataBak> dataList = new List<PP_TriggerDataBak>();

            Expression<Func<PP_TriggerDataBak, bool>> expr = x => true;
            if (isProcess)
            {
                expr = expr.And(x => (x.DataStatus & (int)TriggerDataStatus.Process) == (int)TriggerDataStatus.Process);
                if (isProcessSuccess)
                {
                    expr = expr.And(x => (x.DataStatus & (int)TriggerDataStatus.ProcessSuccess) == (int)TriggerDataStatus.ProcessSuccess);
                }
                else
                {
                    expr = expr.And(x => (x.DataStatus & (int)TriggerDataStatus.ProcessSuccess) != (int)TriggerDataStatus.ProcessSuccess);
                }
            }
            else
            {
                expr = expr.And(x => (x.DataStatus & (int)TriggerDataStatus.Process) != (int)TriggerDataStatus.Process);
            }

            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                dataList = db.PP_TriggerDataBak.Where(expr).OrderBy(x => x.CreateTime).ToList();
            }

            return dataList;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="dataId">主键Id</param>
        /// <param name="dataStatus">待更新的状态值</param>
        /// <returns></returns>
        public bool UpdateStatus(string dataId, int dataStatus)
        {
            bool result = false;
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                var entity = db.PP_TriggerDataBak.Where(x => x.Id.Equals(dataId)).FirstOrDefault();
                entity.DataStatus = dataStatus;
                db.Set<PP_TriggerDataBak>().Attach(entity);
                db.Entry<PP_TriggerDataBak>(entity).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
