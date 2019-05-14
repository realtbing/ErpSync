using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.DbContext;
using Model.Entities.MsSql;

namespace Logic.MsSql
{
    public class WC_UserLogic : BaseLogic
    {
        public WC_User GetSingle(string openId)
        {
            WC_User entity = null;
            using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
            {
                entity = db.WC_Users.Where(x=>x.OpenId.Equals(openId)).FirstOrDefault();
            }
            return entity;
        }

        public bool Add(WC_User entity)
        {
            bool result = false;
            using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
            {
                db.WC_Users.Add(entity);
                if (db.SaveChanges() > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool Update(WC_User entity)
        {
            bool result = false;
            using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
            {
                db.Set<WC_User>().Attach(entity);
                db.Entry<WC_User>(entity).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
