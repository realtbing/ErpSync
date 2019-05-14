namespace Model.Appsettings
{
    public class Appsetting
    {
        /// <summary>
        /// 是否检查抽奖用户与群的关系
        /// </summary>
        public bool IsCheckUserAndCrowdRelation { get; set; }

        /// <summary>
        /// 抽奖时间总量(分钟)
        /// </summary>
        public int LotteryMinute { get; set; }

        /// <summary>
        /// 群用户数量
        /// </summary>
        public int CrowdUserCount { get; set; }
    }
}
