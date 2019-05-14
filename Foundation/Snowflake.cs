using System;

namespace Foundation
{
    /// <summary>
    /// 动态生产有规律的ID 
    /// </summary>
    public class Snowflake
    {
        /// <summary>
        /// 定义一个静态变量来保存类的实例
        /// </summary>
        public static Snowflake snowflake;
        /// <summary>
        /// 机器ID
        /// </summary>
        private static long machineId;
        /// <summary>
        /// 数据ID
        /// </summary>
        private static long datacenterId = 0L;//
        /// <summary>
        /// 计数从零开始
        /// </summary>
        private static long sequence = 0L;//
        /// <summary>
        /// 唯一时间随机量
        /// </summary>
        private static long twepoch = 687888001020L; //
        //机器码字节数
        private static long machineIdBits = 5L; //
        /// <summary>
        /// 数据字节数
        /// </summary>
        private static long datacenterIdBits = 5L;//
        /// <summary>
        /// 最大机器ID
        /// </summary>
        public static long maxMachineId = -1L ^ -1L << (int)machineIdBits; //
        /// <summary>
        /// 最大数据ID
        /// </summary>
        private static long maxDatacenterId = -1L ^ (-1L << (int)datacenterIdBits);//
        /// <summary>
        /// 计数器字节数，12个字节用来保存计数码        
        /// </summary>
        private static long sequenceBits = 12L; //
        /// <summary>
        /// 机器码数据左移位数，就是后面计数器占用的位数
        /// </summary>
        private static long machineIdShift = sequenceBits; //
        private static long datacenterIdShift = sequenceBits + machineIdBits;
        /// <summary>
        /// 时间戳左移动位数就是机器码+计数器总字节数+数据字节数
        /// </summary>
        private static long timestampLeftShift = sequenceBits + machineIdBits + datacenterIdBits; //
        /// <summary>
        /// 一微秒内可以产生计数，如果达到该值则等到下一微妙在进行生成
        /// </summary>
        public static long sequenceMask = -1L ^ -1L << (int)sequenceBits; //
        /// <summary>
        /// 最后时间戳
        /// </summary>
        private static long lastTimestamp = -1L;//
        /// <summary>
        /// 加锁对象
        /// </summary>
        private static object syncRoot = new object();//
        /// <summary>
        /// 定义一个标识确保线程同步
        /// </summary>
        private static readonly object locker = new object();

        public static Snowflake Instance()
        {
            if (snowflake == null)
            {
                // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
                lock (locker)
                {
                    if (snowflake == null)
                    {
                        snowflake = new Snowflake();
                    }
                }
            }
            return snowflake;
        }

        #region 构造函数
        private Snowflake()
        {
            Snowflakes(0L, -1);
        }

        private Snowflake(long machineId)
        {
            Snowflakes(machineId, -1);
        }

        private Snowflake(long machineId, long datacenterId)
        {
            Snowflakes(machineId, datacenterId);
        }

        private void Snowflakes(long machineId, long datacenterId)
        {
            if (machineId >= 0)
            {
                if (machineId > maxMachineId)
                {
                    throw new Exception("机器码ID非法");
                }
                Snowflake.machineId = machineId;
            }
            if (datacenterId >= 0)
            {
                if (datacenterId > maxDatacenterId)
                {
                    throw new Exception("数据中心ID非法");
                }
                Snowflake.datacenterId = datacenterId;
            }
        }
        #endregion

        /// <summary>
        /// 生成当前时间戳
        /// </summary>
        /// <returns>毫秒</returns>
        private static long GetTimestamp()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        /// <summary>
        /// 获取下一微秒时间戳
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns></returns>
        private static long GetNextTimestamp(long lastTimestamp)
        {
            long timestamp = GetTimestamp();
            if (timestamp <= lastTimestamp)
            {
                timestamp = GetTimestamp();
            }
            return timestamp;
        }

        /// <summary>
        /// 获取长整形的ID
        /// </summary>
        /// <returns></returns>
        public long GetId()
        {
            lock (syncRoot)
            {
                long timestamp = GetTimestamp();
                if (Snowflake.lastTimestamp == timestamp)
                { 
                    //同一微秒中生成ID
                    sequence = (sequence + 1) & sequenceMask; //用&运算计算该微秒内产生的计数是否已经到达上限
                    if (sequence == 0)
                    {
                        //一微秒内产生的ID计数已达上限，等待下一微妙
                        timestamp = GetNextTimestamp(Snowflake.lastTimestamp);
                    }
                }
                else
                {
                    //不同微秒生成ID
                    sequence = 0L;
                }
                if (timestamp < lastTimestamp)
                {
                    throw new Exception("时间戳比上一次生成ID时时间戳还小，故异常");
                }
                Snowflake.lastTimestamp = timestamp; //把当前时间戳保存为最后生成ID的时间戳
                long Id = ((timestamp - twepoch) << (int)timestampLeftShift)
                    | (datacenterId << (int)datacenterIdShift)
                    | (machineId << (int)machineIdShift)
                    | sequence;
                return Id;
            }
        }
    }
}
