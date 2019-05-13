using System;
using ServiceStack.Redis;

namespace Foundation.Redis
{
    /// <summary>
    /// Redis操作父类
    /// </summary>
    public abstract class RedisOperatorBase : IDisposable
    {
        protected IRedisClient Redis
        {
            get;
            private set;
        }

        private bool _disposed = false;

        protected RedisOperatorBase()
        {
            Redis = RedisManager.GetClient();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Redis.Dispose();
                    Redis = null;
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            Redis.Save();
        }

        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            Redis.SaveAsync();
        }
    }
}
