using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using ServiceStack.Redis;

namespace Foundation.Redis
{
    public class RedisManager
    {
        /// <summary>
        /// redis配置文件信息
        /// </summary>
        //private static readonly RedisConfigurationSection RedisConfig = RedisConfigurationSection.GetSection();
        private static PooledRedisClientManager _redisClient;

        /// <summary>
        /// 静态构造方法，初始化链接池管理对象
        /// </summary>
        static RedisManager()
        {
            string readServerConnection = "";
            string writeServerConnection = "";
            int maxWritePoolSize = 0;
            int maxReadPoolSize = 0;
            bool autoStart = false;
            long defaultDb = 0;

            CreateManager(readServerConnection, writeServerConnection, maxWritePoolSize, maxReadPoolSize, autoStart, defaultDb);
        }

        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static void CreateManager(string readServerConnection, string writeServerConnection, int maxWritePoolSize, int maxReadPoolSize, bool autoStart, long defaultDb)
        {
            var writeServerList = SplitString(writeServerConnection, ",");
            var readServerList = SplitString(readServerConnection, ",");

            //支持读写分离，均衡负载
            _redisClient = new PooledRedisClientManager(readServerList, writeServerList,
                            new RedisClientManagerConfig
                            {
                                MaxWritePoolSize = maxWritePoolSize,
                                MaxReadPoolSize = maxReadPoolSize,
                                AutoStart = autoStart,
                                DefaultDb = defaultDb,
                            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        private static IEnumerable<string> SplitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }

        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        public static IRedisClient GetClient()
        {
            if (_redisClient == null)
            {
                string readServerConnection = "";
                string writeServerConnection = "";
                int maxWritePoolSize = 0;
                int maxReadPoolSize = 0;
                bool autoStart = false;
                long defaultDb = 0;

                CreateManager(readServerConnection, writeServerConnection, maxWritePoolSize, maxReadPoolSize, autoStart, defaultDb);
            }
            return _redisClient.GetClient();
        }
    }
}
