using System;
using System.Collections.Generic;

namespace Model.Entities.MsSql
{
    public class WC_OfficalAccounts
    {
        public string Id { get; set; }

        public string OfficalId { get; set; }

        public string OfficalName { get; set; }

        public string OfficalCode { get; set; }

        public string OfficalPhoto { get; set; }

        public string OfficalKey { get; set; }

        public string ApiUrl { get; set; }

        public string Token { get; set; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string AccessToken { get; set; }

        public string Remark { get; set; }

        public bool Enable { get; set; }

        public bool IsDefault { get; set; }

        public int Category { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreateBy { get; set; }

        public DateTime ModifyTime { get; set; }

        public string ModifyBy { get; set; }

        #region 导航属性
        public virtual ICollection<WC_User> Users { get; set; }
        #endregion
    }
}
