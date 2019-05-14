using System;

namespace Model.Entities.MsSql
{
    public class WC_User
    {
        public string Id { get; set; }

        public string OpenId { get; set; }

        public string NickName { get; set; }

        public int Sex { get; set; }

        public string Language { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string HeadImgUrl { get; set; }

        public DateTime SubscribeTime { get; set; }

        public string UnionId { get; set; }

        public string Remark { get; set; }

        public string GroupId { get; set; }

        public string TagidList { get; set; }

        public int Subscribe { get; set; }

        public string OfficalAccountId { get; set; }

        public string SessionKey { get; set; }

        #region 导航属性
        public virtual WC_OfficalAccounts OfficalAccount { get; set; }
        #endregion
    }
}
