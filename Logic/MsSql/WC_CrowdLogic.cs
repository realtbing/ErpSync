using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Model.DbContext;
using Model.DTO.MsSql;
using Model.Entities.MsSql;

namespace Logic.MsSql
{
    public class WC_CrowdLogic : BaseLogic
    {

        public WC_CrowdDTO GetSingle(string openId, string openGid, bool isCheckUserAndCrowdRelation)
        {
            WC_CrowdDTO dto = null;
            if (!string.IsNullOrEmpty(openGid) && !string.IsNullOrEmpty(openGid))
            {
                if (isCheckUserAndCrowdRelation)
                {
                    Expression<Func<WC_CrowdUser, bool>> expr = x => x.openId.Equals(openId) && x.openGid.Equals(openGid);
                    using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
                    {
                        var query = db.WC_CrowdUsers.Where(expr);
                        if (query.Count() > 0)
                        {
                            dto = GetWC_Crowd(openGid);
                        }
                    }
                }
                else
                {
                    dto = GetWC_Crowd(openGid);
                }
            }            
            return dto;
        }

        private WC_CrowdDTO GetWC_Crowd(string openGid)
        {
            WC_CrowdDTO dto = null;
            var wcGroup = RedisHelper.Get("WCGroup_" + openGid);
            if (string.IsNullOrEmpty(wcGroup))
            {
                WC_CrowdAndShopDTO tempDto = null;
                Expression<Func<WC_Crowd, bool>> expr = x => x.openGId.Equals(openGid) && x.lotteryTime.HasValue;

                using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
                {
                    tempDto = db.WC_Crowds.Where(expr).Join(db.PP_Shops.Where(x => true), c => c.shopCode, s => s.shopCode, (c, s) => new WC_CrowdAndShopDTO
                    {
                        openGId = c.openGId,
                        name = c.name,
                        shopName = s.shopName,
                        lotteryTime = c.lotteryTime.Value
                    }).FirstOrDefault();
                }
                if (tempDto != null)
                {
                    dto = Mapper.Map<WC_CrowdDTO>(tempDto);
                    RedisHelper.Set("WCGroup_" + openGid, dto);
                }
            }
            else
            {
                dto = Newtonsoft.Json.JsonConvert.DeserializeObject<WC_CrowdDTO>(wcGroup);
            }
            return dto;
        }
    }
}
