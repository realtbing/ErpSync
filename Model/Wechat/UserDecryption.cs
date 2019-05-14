using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Wechat
{
    public class UserDecryption
    {
        /// <summary>
        /// openId
        /// </summary>
        public string openId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string nickName { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// 用户图像
        /// </summary>
        public string avatarUrl { get; set; }

        /// <summary>
        /// 唯一ID
        /// </summary>
        public string unionId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int gender { get; set; }
    }
}
