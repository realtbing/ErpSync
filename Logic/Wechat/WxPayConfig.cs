using Microsoft.Extensions.Options;
using Model.Appsettings;

namespace Logic.Wechat
{
    /// <summary>
    /// 微信相关参数配置文件
    /// </summary>
    public class WxPayConfig
    {
        private static readonly IOptions<Appsetting> _options;
        private static volatile IConfig config;
        private static object syncRoot = new object();

        public static IConfig GetConfig()
        {
            if (config == null)
            {
                lock (syncRoot)
                {
                    if (config == null)
                        config = new WXConfig(_options);
                }
            }
            return config;
        }

    }
}
