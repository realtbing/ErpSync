using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Model.DbContext;
using Model.DTO.MsSql;
using Model.Entities.MsSql;

namespace Logic.MsSql
{
    public class WC_CrowdLuckDrawLogic : BaseLogic
    {
        //public Task Lottery(string openId, string openGid)
        //{
        //    WC_CrowdDTO dto = null;
        //    if (!string.IsNullOrEmpty(openGid) && !string.IsNullOrEmpty(openGid))
        //    {
        //        if (isCheckUserAndCrowdRelation)
        //        {
        //            Expression<Func<WC_CrowdUser, bool>> expr = x => x.openId.Equals(openId) && x.openGid.Equals(openGid);
        //            using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
        //            {
        //                var query = db.WC_CrowdUsers.Where(expr);
        //                if (query.Count() > 0)
        //                {
        //                    dto = GetWC_Crowd(openGid);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            dto = GetWC_Crowd(openGid);
        //        }
        //    }
        //    return dto;
        //}
    }
}
