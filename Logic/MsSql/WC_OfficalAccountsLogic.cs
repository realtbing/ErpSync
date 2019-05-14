using System.Linq;
using Model.DbContext;
using Model.Entities.MsSql;

namespace Logic.MsSql
{
    public class WC_OfficalAccountsLogic : BaseLogic
    {
        public WC_OfficalAccounts GetSingle(string appId)
        {
            WC_OfficalAccounts entity = null;
            using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
            {
                entity = db.WC_OfficalAccountses.Where(x => x.AppId.Equals(appId)).FirstOrDefault();
            }
            return entity;
        }
    }
}
