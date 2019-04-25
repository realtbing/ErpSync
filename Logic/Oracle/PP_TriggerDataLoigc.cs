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
    public class PP_TriggerDataLoigc : BaseLogic
    {
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="isProcess">是否处理</param>
        /// <param name="isProcessSuccess">处理是否成功</param>
        /// <returns></returns>
        public List<PP_TriggerData> GetList(bool isProcess, bool isProcessSuccess)
        {
            List<PP_TriggerData> dataList = new List<PP_TriggerData>();

            Expression<Func<PP_TriggerData, bool>> expr = x => true;
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
                dataList = db.PP_TriggerData.Where(expr).OrderBy(x => x.CreateTime).ToList();
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
                var entity = db.PP_TriggerData.Where(x => x.Id.Equals(dataId)).FirstOrDefault();
                entity.DataStatus = dataStatus;
                db.Set<PP_TriggerData>().Attach(entity);
                db.Entry<PP_TriggerData>(entity).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
