using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using AutoMapper;
using Model.Appsettings;
using Model.DbContext;
using Model.DTO.MsSql;
using Model.Entities.MsSql;

namespace Logic.MsSql
{
    public class WC_CrowdLogic : BaseLogic
    {
        private readonly IOptions<Appsetting> _options;
        public WC_CrowdLogic(IOptions<Appsetting> options)
        {
            _options = options;
        }

        public bool Add(WC_Crowd entity)
        {
            bool result = false;
            using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
            {
                db.WC_Crowds.Add(entity);
                if (db.SaveChanges() > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public WC_Crowd GetSingle(string openGid)
        {
            WC_Crowd entity = null;
            using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
            {
                entity = db.WC_Crowds.Where(x=>x.openGId.Equals(openGid)).FirstOrDefault();
            }
            return entity;
        }

        public WC_CrowdDTO GetSingle(string openId, string openGid)
        {
            WC_CrowdDTO dto = null;
            if (!string.IsNullOrEmpty(openGid) && !string.IsNullOrEmpty(openGid))
            {
                WC_CrowdUser crowdUserEntity = null;
                var wcUser = RedisHelper.HGet("WCUser", openId);
                if (string.IsNullOrEmpty(wcUser))
                {
                    using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
                    {
                        var query = db.WC_CrowdUsers.Where(x => x.openId.Equals(openId) && x.status == 1);
                        if (query.Count() > 0)
                        {
                            crowdUserEntity = query.FirstOrDefault();
                        }
                    }
                    if (crowdUserEntity != null)
                    {
                        RedisHelper.HSet("WCUser", openId, crowdUserEntity);
                    }
                    else
                    {
                        return dto;
                    }
                }
                else
                {
                    crowdUserEntity = Newtonsoft.Json.JsonConvert.DeserializeObject<WC_CrowdUser>(wcUser);
                }

                if (_options.Value.IsCheckUserAndCrowdRelation)
                {
                    if (crowdUserEntity.openGid.Equals(openGid))
                    {
                        dto = GetWC_Crowd(openGid);
                    }
                }
                else
                {
                    dto = GetWC_Crowd(openGid);
                }

                if (dto != null)
                {
                    var wcGroupLotteryNumberList = RedisHelper.HGet("LotteryNumberList", openGid);
                    if (string.IsNullOrEmpty(wcGroupLotteryNumberList))
                    {
                        List<int> groupLotteryNumberList = new List<int>();
                        for (var i = 1; i <= _options.Value.CrowdUserCount; i++)
                        {
                            groupLotteryNumberList.Add(i);
                        }

                        RedisHelper.HSet("LotteryNumberList", openGid, groupLotteryNumberList);
                    }
                }
            }
            return dto;
        }

        private WC_CrowdDTO GetWC_Crowd(string openGid)
        {
            WC_CrowdDTO dto = null;
            var wcGroup = RedisHelper.HGet("WCGroup", openGid);
            if (string.IsNullOrEmpty(wcGroup))
            {
                WC_CrowdAndShopDTO tempDto = null;
                Expression<Func<WC_Crowd, bool>> expr = x => x.openGId.Equals(openGid) && x.lotteryTime.HasValue && x.joinPeople > 0 && x.winners > 0 && x.status == 1;

                using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
                {
                    tempDto = db.WC_Crowds.Where(expr).Join(db.PP_Shops.Where(x => true), c => c.shopCode, s => s.shopCode, (c, s) => new WC_CrowdAndShopDTO
                    {
                        openGId = c.openGId,
                        name = c.name,
                        shopCode = s.shopCode,
                        shopName = s.shopName,
                        lotteryTime = c.lotteryTime.Value,
                        lotteryMinute = _options.Value.LotteryMinute,
                        allowLotteryPepoleNumber = c.joinPeople,
                        allowDrawPepoleNumber = c.winners
                    }).FirstOrDefault();
                }
                if (tempDto != null)
                {
                    dto = Mapper.Map<WC_CrowdDTO>(tempDto);
                    RedisHelper.HSet("WCGroup", openGid, tempDto);
                }
            }
            else
            {
                dto = Mapper.Map<WC_CrowdDTO>(Newtonsoft.Json.JsonConvert.DeserializeObject<WC_CrowdAndShopDTO>(wcGroup));
            }
            return dto;
        }
    }
}
