using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model.Appsettings;
using Model.DbContext;
using Model.DTO.MsSql;
using Model.Entities.MsSql;
using Model.ViewModel;

namespace Logic.MsSql
{
    public class WC_CrowdLuckDrawLogic : BaseLogic
    {
        /// <summary>
        /// 定义一个标识确保线程同步
        /// </summary>
        private static readonly object locker = new object();

        private readonly IOptions<Appsetting> _options;
        public WC_CrowdLuckDrawLogic(IOptions<Appsetting> options)
        {
            _options = options;
        }

        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="openId">用户OpenId</param>
        /// <param name="openGid">群Id</param>
        /// <returns>1:抽奖成功;2:抽奖群信息异常;3:用户与群不匹配;4:抽奖尚未开始;5:抽奖已结束;6:抽奖名额已满;7:用户不存在;99:抽奖失败</returns>
        public int Lottery(LotteryVM model)
        {
            if (!string.IsNullOrWhiteSpace(model.openId) && !string.IsNullOrEmpty(model.openGid))
            {
                WC_CrowdUser crowdUserEntity = null;
                #region 校验用户
                var wcUser = RedisHelper.HGet("WCUser", model.openId);
                if (!string.IsNullOrEmpty(wcUser))
                {
                    crowdUserEntity = Newtonsoft.Json.JsonConvert.DeserializeObject<WC_CrowdUser>(wcUser);
                }
                else
                {
                    return 7;
                }
                #endregion
                #region 检查用户与群的关系
                if (_options.Value.IsCheckUserAndCrowdRelation)
                {
                    if (!crowdUserEntity.openGid.Equals(model.openGid))
                    {
                        return 3;
                    }
                }
                #endregion
                #region 检查群抽奖相关信息
                WC_CrowdAndShopDTO crowdAndShopDto = null;
                var wcGroup = RedisHelper.HGet("WCGroup", model.openGid);
                if (!string.IsNullOrEmpty(wcGroup))
                {
                    crowdAndShopDto = Newtonsoft.Json.JsonConvert.DeserializeObject<WC_CrowdAndShopDTO>(wcGroup);
                }
                else
                {
                    return 2;
                }
                #endregion
                #region 检查抽奖时间
                var lotteryTime = DateTime.Now.Date.AddHours(crowdAndShopDto.lotteryTime.Hour)
                                                   .AddMinutes(crowdAndShopDto.lotteryTime.Minute)
                                                   .AddSeconds(crowdAndShopDto.lotteryTime.Second);
                if (lotteryTime > DateTime.Now)
                {
                    //抽奖未开始
                    return 4;
                }
                if (lotteryTime.AddMinutes(crowdAndShopDto.lotteryMinute) < DateTime.Now)
                {
                    //抽奖已结束
                    return 5;
                }
                #endregion
                #region 单进程处理抽奖号码
                int lotteryNunber = 0;
                lock (locker)
                {
                    //获取群抽奖号码池
                    var wcGroupLotteryNumberList = RedisHelper.HGet("LotteryNumberList", model.openGid);
                    if (!string.IsNullOrEmpty(wcGroupLotteryNumberList))
                    {
                        var lotteryNumberList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(wcGroupLotteryNumberList);

                        if (lotteryNumberList.Count <= _options.Value.CrowdUserCount - crowdAndShopDto.allowLotteryPepoleNumber)
                        {
                            return 6;
                        }

                        Random rndNumber = new Random();
                        var lotteryNumberListIndex = rndNumber.Next(0, lotteryNumberList.Count);
                        lotteryNunber = lotteryNumberList[lotteryNumberListIndex];
                        lotteryNumberList.Remove(lotteryNunber);

                        RedisHelper.HSet("LotteryNumberList", model.openGid, lotteryNumberList);
                    }
                }
                #endregion
                #region 抽奖结果存储
                if (lotteryNunber > 0)
                {
                    new Thread(() =>
                    {
                        List<WC_CrowdLuckDraw> lotteryUserList = new List<WC_CrowdLuckDraw>();
                        var wcGroupLotteryUserList = RedisHelper.HGet("LotteryUserList", model.openGid);
                        if (!string.IsNullOrEmpty(wcGroupLotteryUserList))
                        {
                            lotteryUserList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WC_CrowdLuckDraw>>(wcGroupLotteryUserList);
                        }
                        WC_CrowdLuckDraw entity = new WC_CrowdLuckDraw();
                        entity.shopCode = crowdAndShopDto.shopCode;
                        entity.shopName = crowdAndShopDto.shopName;
                        entity.openGid = crowdAndShopDto.openGId;
                        entity.groupName = crowdAndShopDto.name;
                        entity.code = Guid.NewGuid().ToString();
                        entity.openId = model.openId;
                        entity.userName = crowdUserEntity.userName;
                        entity.headPicture = crowdUserEntity.headPicture;
                        entity.luckyNumber = lotteryNunber;
                        entity.winning = 2;
                        entity.date = DateTime.Now.ToString("yyyy-MM-dd");
                        entity.createTime = DateTime.Now;
                        lotteryUserList.Add(entity);

                        if (lotteryUserList.Count == crowdAndShopDto.allowLotteryPepoleNumber)
                        {
                            List<WC_CrowdLuckDraw> winnerList = new List<WC_CrowdLuckDraw>();
                            for (var i = 0; i < crowdAndShopDto.allowDrawPepoleNumber; i++)
                            {
                                Random rndUser = new Random();
                                var lotteryUserListIndex = rndUser.Next(0, lotteryUserList.Count);
                                var winner = lotteryUserList[lotteryUserListIndex];
                                lotteryUserList.Remove(winner);
                                winnerList.Add(winner);
                            }
                            foreach (var winner in winnerList)
                            {
                                winner.winning = 1;
                                winner.winnerTime = DateTime.Now;
                                lotteryUserList.Add(winner);
                            }
                            using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
                            {
                                db.WC_CrowdLuckDraws.AddRange(lotteryUserList);
                                db.SaveChanges();
                            }
                        }
                        RedisHelper.HSet("LotteryUserList", model.openGid, lotteryUserList);
                    }).Start();

                    return 1;
                }
                else
                {
                    return 99;
                }
                #endregion
            }
            else
            {
                return 99;
            }
        }

        public WC_CrowdLuckDrawResultDTO GetResult(string openId, string openGid)
        {
            WC_CrowdLuckDrawResultDTO dto = null;
            var wcGroup = RedisHelper.HGet("WCGroup", openGid);
            var wcGroupLotteryUserList = RedisHelper.HGet("LotteryUserList", openGid);

            if (!string.IsNullOrEmpty(wcGroup) && !string.IsNullOrEmpty(wcGroupLotteryUserList))
            {
                var crowdAndShopDto = Newtonsoft.Json.JsonConvert.DeserializeObject<WC_CrowdAndShopDTO>(wcGroup);

                var lotteryUserList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WC_CrowdLuckDraw>>(wcGroupLotteryUserList);

                var lotteryResult = lotteryUserList.Find(x => x.openId.Equals(openId));
                if (crowdAndShopDto != null && lotteryResult != null)
                {
                    dto = new WC_CrowdLuckDrawResultDTO();
                    dto.name = crowdAndShopDto.name;
                    dto.lotteryNumber = lotteryResult.luckyNumber;
                    dto.lotteryCount = lotteryUserList.Count;
                    dto.lotteryRecord = Mapper.Map<List<WC_CrowdLuckDrawDTO>>(lotteryUserList);
                }
            }
            return dto;
        }

        public WC_CrowdLuckDrawHistoryResultDTO GetHistoryResult(DateTime lotteryDate, string openGid)
        {
            WC_CrowdLuckDrawHistoryResultDTO result = null;
            List<WC_CrowdLuckDrawDTO> dtoList = new List<WC_CrowdLuckDrawDTO>();
            using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
            {
                var list = db.WC_CrowdLuckDraws.Where(x => x.openGid.Equals(openGid) && x.createTime.Date == lotteryDate).ToList();
                list.GroupBy(x => x.date).Select(g => new WC_CrowdLuckDrawHistoryResultDTO
                {
                    name = list.Where(x => x.date.Equals(g.Key)).FirstOrDefault().groupName,
                    lotteryDate = g.Key,
                    lotteryCount = list.Count,
                    lotteryRecord = Mapper.Map<List<WC_CrowdLuckDrawDTO>>(list)
                });
            }
            return result;
        }

        public void LotteryStatistics()
        {
            var list = new List<WC_Crowd>();
            using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
            {
                list = db.WC_Crowds.Where(x => x.lotteryTime.HasValue && x.joinPeople > 0 && x.winners > 0 && x.status == 1).ToList();
            }
            using (MsSqlDbContext db = new MsSqlDbContext(base.mssqlBuilder.Options))
            {
                foreach (var temp in list)
                {
                    var WC_CrowdLuckDrawEntityList = db.WC_CrowdLuckDraws.Where(x => x.openGid.Equals(temp.openGId) && x.createTime.Date == DateTime.Now.Date).ToList();
                    if (!WC_CrowdLuckDrawEntityList.Exists(x => x.winning == 1))
                    {
                        List<WC_CrowdLuckDraw> winnerList = new List<WC_CrowdLuckDraw>();
                        for (var i = 0; i < temp.winners; i++)
                        {
                            Random rndUser = new Random();
                            var lotteryUserListIndex = rndUser.Next(0, WC_CrowdLuckDrawEntityList.Count);
                            var winner = WC_CrowdLuckDrawEntityList[lotteryUserListIndex];
                            WC_CrowdLuckDrawEntityList.Remove(winner);
                            winnerList.Add(winner);
                        }
                        foreach (var winner in winnerList)
                        {
                            winner.winning = 1;
                            winner.winnerTime = DateTime.Now;
                            db.Set<WC_CrowdLuckDraw>().Attach(winner);
                            db.Entry<WC_CrowdLuckDraw>(winner).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
