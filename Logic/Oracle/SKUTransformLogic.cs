using System.Collections.Generic;
using System.Linq;
using Model.DbContext;
using Model.Entities.Oracle;

namespace Logic.Oracle
{
    public class SKUTransformLogic : BaseLogic
    {
        public List<SKUTransform> GetList(List<string> skuList)
        {
            List<SKUTransform> result = new List<SKUTransform>();
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.SKUTransforms.Where(x=> skuList.Contains(x.MOTHERSKUCODE) || skuList.Contains(x.CHILDSKUCODE)).ToList();
            }

            return result;
        }

        public List<SKUTransform> GetChildList(string motherSKUCode)
        {
            List<SKUTransform> result = new List<SKUTransform>();
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.SKUTransforms.Where(x => x.MOTHERSKUCODE.Equals(motherSKUCode)).ToList();
            }

            return result;
        }

        public List<SKUTransform> GetListByMotherCode(string goodsCode)
        {
            List<SKUTransform> result = new List<SKUTransform>();
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.SKUTransforms.Where(x => x.MOTHERSKUCODE.Equals(goodsCode)).ToList();
            }

            return result;
        }

        public List<SKUTransform> GetListByChildCode(string goodsCode)
        {
            List<SKUTransform> result = new List<SKUTransform>();
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.SKUTransforms.Where(x => x.CHILDSKUCODE.Equals(goodsCode)).ToList();
            }

            return result;
        }

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public SKUTransform GetSingleById(long Id)
        {
            SKUTransform result = null;
            using (OracleDbContext db = new OracleDbContext(base.oracleBuilder.Options))
            {
                result = db.SKUTransforms.Where(x => x.ID == Id).FirstOrDefault();
            }
            return result;
        }
    }
}
