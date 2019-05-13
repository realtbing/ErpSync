using System;
using System.Collections.Generic;
using ServiceStack.Text;

namespace Foundation.Redis
{
    /// <summary>
    /// Redis操作仓储
    /// </summary>
    public class RedisRepository : RedisOperatorBase
    {
        #region String

        /// <summary>
        /// 设置单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Item_Set<T>(string key, T t)
        {
            return Redis.Set(key, t);
        }

        /// <summary>
        /// 获取单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Item_Get<T>(string key)
        {
            return Redis.Get<T>(key);
        }

        /// <summary>
        /// 移除单体
        /// </summary>
        /// <param name="key"></param>
        public bool Item_Remove(string key)
        {
            return Redis.Remove(key);
        }

        #endregion

        #region List

        /// <summary>
        /// 添加数据到List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        public void List_Add<T>(string key, T t)
        {
            var redisTypedClient = Redis.As<T>();
            redisTypedClient.AddItemToList(redisTypedClient.Lists[key], t);
        }

        /// <summary>
        /// 从List中移除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool List_Remove<T>(string key, T t)
        {
            var redisTypedClient = Redis.As<T>();
            return redisTypedClient.RemoveItemFromList(redisTypedClient.Lists[key], t) > 0;
        }

        /// <summary>
        /// 从List中移除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public void List_RemoveAt<T>(string key, int index)
        {
            var redisTypedClient = Redis.As<T>();
            redisTypedClient.Lists[key].RemoveAt(index);
            //return redisTypedClient.RemoveItemFromList(redisTypedClient.Lists[key], t) > 0;
        }

        /// <summary>
        /// 移除List的所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        public void List_RemoveAll<T>(string key)
        {
            var redisTypedClient = Redis.As<T>();
            redisTypedClient.Lists[key].RemoveAll();
        }

        /// <summary>
        /// 获取List的长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int List_Count(string key)
        {
            return (int)Redis.GetListCount(key);
        }

        /// <summary>
        /// 获取指定范围中List的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<T> List_GetRange<T>(string key, int start, int count)
        {
            var redisTypedClient = Redis.As<T>();
            return redisTypedClient.Lists[key].GetRange(start, start + count - 1);
        }

        /// <summary>
        /// 获取List的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> List_GetList<T>(string key)
        {
            var redisTypedClient = Redis.As<T>();
            return redisTypedClient.Lists[key].GetRange(0, redisTypedClient.Lists[key].Count);
        }

        /// <summary>
        /// 获取List的分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> List_GetList<T>(string key, int pageIndex, int pageSize)
        {
            int start = pageSize * (pageIndex - 1);
            return List_GetRange<T>(key, start, pageSize);
        }

        public void EnqueueItemOnList<T>(string key, T t)
        {
            var value = JsonSerializer.SerializeToString(t);
            Redis.EnqueueItemOnList(key, value);
        }

        public T DequeueItemOnList<T>(string key)
        {
            var redisTypedClient = Redis.As<T>();
            return redisTypedClient.DequeueItemFromList(redisTypedClient.Lists[key]);
        }

        public void PushItemToList<T>(string key,T t)
        {
            var value = JsonSerializer.SerializeToString(t);
            Redis.PushItemToList(key,value);
        }

        public T PopItemFromList<T>(string key)
        {
            var redisTypedClient = Redis.As<T>();
            return redisTypedClient.PopItemFromList(redisTypedClient.Lists[key]);
        }


        #endregion

        #region Set

        /// <summary>
        /// 添加数据到Set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        public void Set_Add<T>(string key, T t)
        {
            var redisTypedClient = Redis.As<T>();
            redisTypedClient.Sets[key].Add(t);
        }

        /// <summary>
        /// 判断数据是否已经被缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Set_Contains<T>(string key, T t)
        {
            var redisTypedClient = Redis.As<T>();
            return redisTypedClient.Sets[key].Contains(t);
        }

        /// <summary>
        /// 从Set中移除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Set_Remove<T>(string key, T t)
        {
            var redisTypedClient = Redis.As<T>();
            return redisTypedClient.Sets[key].Remove(t);
        }

        #endregion

        #region Hash
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="filed"></param>
        /// <returns></returns>
        public bool Hash_Exist(string key, string filed)
        {
            return Redis.HashContainsEntry(key, filed);
        }

        /// <summary>
        /// 存储数据到Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="filed"></param>
        /// <returns></returns>
        public bool Hash_Set<T>(string key, string filed, T t)
        {
            string value = JsonSerializer.SerializeToString(t);
            return Redis.SetEntryInHash(key, filed, value);
        }

        /// <summary>
        /// 从Hash中移除数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="filed"></param>
        /// <returns></returns>
        public bool Hash_Remove(string key, string filed)
        {
            return Redis.RemoveEntryFromHash(key, filed);
        }

        /// <summary>
        /// 移除整个Hash的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Hash_Remove(string key)
        {
            return Redis.Remove(key);
        }

        /// <summary>
        /// 获取Hash的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="filed"></param>
        /// <returns></returns>
        public T Hash_Get<T>(string key, string filed)
        {
            string value = Redis.GetValueFromHash(key, filed);
            return JsonSerializer.DeserializeFromString<T>(value);
        }

        /// <summary>
        /// 获取Hash的全部数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> Hash_GetAll<T>(string key)
        {
            var result = new List<T>();
            var list = Redis.GetHashValues(key);
            if (list != null && list.Count > 0)
            {
                list.ForEach(item =>
                {
                    var value = JsonSerializer.DeserializeFromString<T>(item);
                    result.Add(value);
                });
            }
            return result;
        }

        #endregion

        #region SortedSet
        /// <summary>
        ///  添加数据到SortedSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="score"></param>
        public bool SortedSet_Add<T>(string key, T t, double score)
        {
            string value = JsonSerializer.SerializeToString<T>(t);
            return Redis.AddItemToSortedSet(key, value, score);
        }

        /// <summary>
        /// 从SortedSet中移除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool SortedSet_Remove<T>(string key, T t)
        {
            string value = JsonSerializer.SerializeToString<T>(t);
            return Redis.RemoveItemFromSortedSet(key, value);
        }

        /// <summary>
        /// 修剪SortedSet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="size">保留的条数</param>
        /// <returns></returns>
        public int SortedSet_Trim(string key, int size)
        {
            return (int)Redis.RemoveRangeFromSortedSet(key, size, 9999999);
        }

        /// <summary>
        /// 获取SortedSet的长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int SortedSet_Count(string key)
        {
            return (int)Redis.GetSortedSetCount(key);
        }

        /// <summary>
        /// 获取SortedSet的分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> SortedSet_GetList<T>(string key, int pageIndex, int pageSize)
        {
            var list = Redis.GetRangeFromSortedSet(key, (pageIndex - 1) * pageSize, pageIndex * pageSize - 1);
            if (list != null && list.Count > 0)
            {
                var result = new List<T>();
                list.ForEach(item =>
                {
                    var data = JsonSerializer.DeserializeFromString<T>(item);
                    result.Add(data);
                });
                return result;
            }
            return null;
        }


        /// <summary>
        /// 获取SortedSet的全部数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> SortedSet_GetListALL<T>(string key)
        {
            var list = Redis.GetRangeFromSortedSet(key, 0, 9999999);
            if (list != null && list.Count > 0)
            {
                var result = new List<T>();
                list.ForEach(item =>
                {
                    var data = JsonSerializer.DeserializeFromString<T>(item);
                    result.Add(data);
                });
                return result;
            }
            return null;
        }

        //public  double SortedSetGetItemScore<T>(string key,T t)
        //{
        //    
        //    {
        //        var data = JsonSerializer.SerializeToString<T>(t);
        //        return Redis.GetItemScoreInSortedSet(key, data);
        //    }
        //    return 0;
        //}

        #endregion

        /// <summary>
        /// 设置缓存过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datetime"></param>
        public void SetExpireTime(string key, DateTime datetime)
        {
            Redis.ExpireEntryAt(key, datetime);
        }
    }
}
