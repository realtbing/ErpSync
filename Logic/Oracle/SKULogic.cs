using System.Linq;
using Model.DbContext;
using Model.Entities.Oracle;

namespace Logic.Oracle
{
    public class SKULogic : BaseLogic
    {
        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public SKU GetSingleById(long Id)
        {
            SKU result = null;
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.SKU.Where(x => x.ID == Id).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public SKU GetSingleByCode(string code)
        {
            SKU result = null;
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.SKU.Where(x => x.CODE.Equals(code)).FirstOrDefault();
            }
            return result;
        }
    }
}
