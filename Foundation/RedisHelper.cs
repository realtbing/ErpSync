using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foundation
{
    public class RedisHelper
    {
        //public RedisHelper(IConfigurationRoot config)
        //{

        //}

        //#region String 操作

        ///// <summary>
        ///// 设置 key 并保存字符串（如果 key 已存在，则覆盖值）
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisValue"></param>
        ///// <param name="expiry"></param>
        ///// <returns></returns>
        //public bool StringSet(string redisKey, string redisValue, TimeSpan? expiry = null)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.StringSet(redisKey, redisValue, expiry);
        //}

        ///// <summary>
        ///// 保存多个 Key-value
        ///// </summary>
        ///// <param name="keyValuePairs"></param>
        ///// <returns></returns>
        //public bool StringSet(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValuePairs)
        //{
        //    keyValuePairs =
        //        keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(AddKeyPrefix(x.Key), x.Value));
        //    return _db.StringSet(keyValuePairs.ToArray());
        //}

        ///// <summary>
        ///// 获取字符串
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="expiry"></param>
        ///// <returns></returns>
        //public string StringGet(string redisKey, TimeSpan? expiry = null)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.StringGet(redisKey);
        //}

        ///// <summary>
        ///// 存储一个对象（该对象会被序列化保存）
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisValue"></param>
        ///// <param name="expiry"></param>
        ///// <returns></returns>
        //public bool StringSet<T>(string redisKey, T redisValue, TimeSpan? expiry = null)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    var json = Serialize(redisValue);
        //    return _db.StringSet(redisKey, json, expiry);
        //}

        ///// <summary>
        ///// 获取一个对象（会进行反序列化）
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="expiry"></param>
        ///// <returns></returns>
        //public T StringGet<T>(string redisKey, TimeSpan? expiry = null)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return Deserialize<T>(_db.StringGet(redisKey));
        //}

        //#region async

        ///// <summary>
        ///// 保存一个字符串值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisValue"></param>
        ///// <param name="expiry"></param>
        ///// <returns></returns>
        //public async Task<bool> StringSetAsync(string redisKey, string redisValue, TimeSpan? expiry = null)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.StringSetAsync(redisKey, redisValue, expiry);
        //}

        ///// <summary>
        ///// 保存一组字符串值
        ///// </summary>
        ///// <param name="keyValuePairs"></param>
        ///// <returns></returns>
        //public async Task<bool> StringSetAsync(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValuePairs)
        //{
        //    keyValuePairs =
        //        keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(AddKeyPrefix(x.Key), x.Value));
        //    return await _db.StringSetAsync(keyValuePairs.ToArray());
        //}

        ///// <summary>
        ///// 获取单个值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisValue"></param>
        ///// <param name="expiry"></param>
        ///// <returns></returns>
        //public async Task<string> StringGetAsync(string redisKey, string redisValue, TimeSpan? expiry = null)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.StringGetAsync(redisKey);
        //}

        ///// <summary>
        ///// 存储一个对象（该对象会被序列化保存）
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisValue"></param>
        ///// <param name="expiry"></param>
        ///// <returns></returns>
        //public async Task<bool> StringSetAsync<T>(string redisKey, T redisValue, TimeSpan? expiry = null)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    var json = Serialize(redisValue);
        //    return await _db.StringSetAsync(redisKey, json, expiry);
        //}

        ///// <summary>
        ///// 获取一个对象（会进行反序列化）
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="expiry"></param>
        ///// <returns></returns>
        //public async Task<T> StringGetAsync<T>(string redisKey, TimeSpan? expiry = null)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return Deserialize<T>(await _db.StringGetAsync(redisKey));
        //}

        //#endregion async

        //#endregion String 操作

        //#region Hash 操作

        ///// <summary>
        ///// 判断该字段是否存在 hash 中
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <returns></returns>
        //public bool HashExists(string redisKey, string hashField)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.HashExists(redisKey, hashField);
        //}

        ///// <summary>
        ///// 从 hash 中移除指定字段
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <returns></returns>
        //public bool HashDelete(string redisKey, string hashField)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.HashDelete(redisKey, hashField);
        //}

        ///// <summary>
        ///// 从 hash 中移除指定字段
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <returns></returns>
        //public long HashDelete(string redisKey, IEnumerable<RedisValue> hashField)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.HashDelete(redisKey, hashField.ToArray());
        //}

        ///// <summary>
        ///// 在 hash 设定值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool HashSet(string redisKey, string hashField, string value)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.HashSet(redisKey, hashField, value);
        //}

        ///// <summary>
        ///// 在 hash 中设定值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashFields"></param>
        //public void HashSet(string redisKey, IEnumerable<HashEntry> hashFields)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    _db.HashSet(redisKey, hashFields.ToArray());
        //}

        ///// <summary>
        ///// 在 hash 中获取值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <returns></returns>
        //public RedisValue HashGet(string redisKey, string hashField)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.HashGet(redisKey, hashField);
        //}

        ///// <summary>
        ///// 在 hash 中获取值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public RedisValue[] HashGet(string redisKey, RedisValue[] hashField, string value)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.HashGet(redisKey, hashField);
        //}

        ///// <summary>
        ///// 从 hash 返回所有的字段值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public IEnumerable<RedisValue> HashKeys(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.HashKeys(redisKey);
        //}

        ///// <summary>
        ///// 返回 hash 中的所有值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public RedisValue[] HashValues(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.HashValues(redisKey);
        //}

        ///// <summary>
        ///// 在 hash 设定值（序列化）
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool HashSet<T>(string redisKey, string hashField, T value)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    var json = Serialize(value);
        //    return _db.HashSet(redisKey, hashField, json);
        //}

        ///// <summary>
        ///// 在 hash 中获取值（反序列化）
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <returns></returns>
        //public T HashGet<T>(string redisKey, string hashField)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return Deserialize<T>(_db.HashGet(redisKey, hashField));
        //}

        //#region async

        ///// <summary>
        ///// 判断该字段是否存在 hash 中
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <returns></returns>
        //public async Task<bool> HashExistsAsync(string redisKey, string hashField)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.HashExistsAsync(redisKey, hashField);
        //}

        ///// <summary>
        ///// 从 hash 中移除指定字段
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <returns></returns>
        //public async Task<bool> HashDeleteAsync(string redisKey, string hashField)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.HashDeleteAsync(redisKey, hashField);
        //}

        ///// <summary>
        ///// 从 hash 中移除指定字段
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <returns></returns>
        //public async Task<long> HashDeleteAsync(string redisKey, IEnumerable<RedisValue> hashField)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.HashDeleteAsync(redisKey, hashField.ToArray());
        //}

        ///// <summary>
        ///// 在 hash 设定值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public async Task<bool> HashSetAsync(string redisKey, string hashField, string value)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.HashSetAsync(redisKey, hashField, value);
        //}

        ///// <summary>
        ///// 在 hash 中设定值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashFields"></param>
        //public async Task HashSetAsync(string redisKey, IEnumerable<HashEntry> hashFields)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    await _db.HashSetAsync(redisKey, hashFields.ToArray());
        //}

        ///// <summary>
        ///// 在 hash 中获取值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <returns></returns>
        //public async Task<RedisValue> HashGetAsync(string redisKey, string hashField)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.HashGetAsync(redisKey, hashField);
        //}

        ///// <summary>
        ///// 在 hash 中获取值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public async Task<IEnumerable<RedisValue>> HashGetAsync(string redisKey, RedisValue[] hashField, string value)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.HashGetAsync(redisKey, hashField);
        //}

        ///// <summary>
        ///// 从 hash 返回所有的字段值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public async Task<IEnumerable<RedisValue>> HashKeysAsync(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.HashKeysAsync(redisKey);
        //}

        ///// <summary>
        ///// 返回 hash 中的所有值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public async Task<IEnumerable<RedisValue>> HashValuesAsync(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.HashValuesAsync(redisKey);
        //}

        ///// <summary>
        ///// 在 hash 设定值（序列化）
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public async Task<bool> HashSetAsync<T>(string redisKey, string hashField, T value)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    var json = Serialize(value);
        //    return await _db.HashSetAsync(redisKey, hashField, json);
        //}

        ///// <summary>
        ///// 在 hash 中获取值（反序列化）
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="hashField"></param>
        ///// <returns></returns>
        //public async Task<T> HashGetAsync<T>(string redisKey, string hashField)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return Deserialize<T>(await _db.HashGetAsync(redisKey, hashField));
        //}

        //#endregion async

        //#endregion Hash 操作

        //#region List 操作

        ///// <summary>
        ///// 移除并返回存储在该键列表的第一个元素
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public string ListLeftPop(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.ListLeftPop(redisKey);
        //}

        ///// <summary>
        ///// 移除并返回存储在该键列表的最后一个元素
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public string ListRightPop(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.ListRightPop(redisKey);
        //}

        ///// <summary>
        ///// 移除列表指定键上与该值相同的元素
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisValue"></param>
        ///// <returns></returns>
        //public long ListRemove(string redisKey, string redisValue)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.ListRemove(redisKey, redisValue);
        //}

        ///// <summary>
        ///// 在列表尾部插入值。如果键不存在，先创建再插入值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisValue"></param>
        ///// <returns></returns>
        //public long ListRightPush(string redisKey, string redisValue)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.ListRightPush(redisKey, redisValue);
        //}

        ///// <summary>
        ///// 在列表头部插入值。如果键不存在，先创建再插入值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisValue"></param>
        ///// <returns></returns>
        //public long ListLeftPush(string redisKey, string redisValue)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.ListLeftPush(redisKey, redisValue);
        //}

        ///// <summary>
        ///// 返回列表上该键的长度，如果不存在，返回 0
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public long ListLength(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.ListLength(redisKey);
        //}

        ///// <summary>
        ///// 返回在该列表上键所对应的元素
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public IEnumerable<RedisValue> ListRange(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.ListRange(redisKey);
        //}

        ///// <summary>
        ///// 移除并返回存储在该键列表的第一个元素
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public T ListLeftPop<T>(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return Deserialize<T>(_db.ListLeftPop(redisKey));
        //}

        ///// <summary>
        ///// 移除并返回存储在该键列表的最后一个元素
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public T ListRightPop<T>(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return Deserialize<T>(_db.ListRightPop(redisKey));
        //}

        ///// <summary>
        ///// 在列表尾部插入值。如果键不存在，先创建再插入值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisValue"></param>
        ///// <returns></returns>
        //public long ListRightPush<T>(string redisKey, T redisValue)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.ListRightPush(redisKey, Serialize(redisValue));
        //}

        ///// <summary>
        ///// 在列表头部插入值。如果键不存在，先创建再插入值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisValue"></param>
        ///// <returns></returns>
        //public long ListLeftPush<T>(string redisKey, T redisValue)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.ListLeftPush(redisKey, Serialize(redisValue));
        //}

        //#region List-async

        ///// <summary>
        ///// 移除并返回存储在该键列表的第一个元素
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public async Task<string> ListLeftPopAsync(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.ListLeftPopAsync(redisKey);
        //}

        ///// <summary>
        ///// 移除并返回存储在该键列表的最后一个元素
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public async Task<string> ListRightPopAsync(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.ListRightPopAsync(redisKey);
        //}

        ///// <summary>
        ///// 移除列表指定键上与该值相同的元素
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisValue"></param>
        ///// <returns></returns>
        //public async Task<long> ListRemoveAsync(string redisKey, string redisValue)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.ListRemoveAsync(redisKey, redisValue);
        //}

        ///// <summary>
        ///// 在列表尾部插入值。如果键不存在，先创建再插入值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisValue"></param>
        ///// <returns></returns>
        //public async Task<long> ListRightPushAsync(string redisKey, string redisValue)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.ListRightPushAsync(redisKey, redisValue);
        //}

        ///// <summary>
        ///// 在列表头部插入值。如果键不存在，先创建再插入值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisValue"></param>
        ///// <returns></returns>
        //public async Task<long> ListLeftPushAsync(string redisKey, string redisValue)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.ListLeftPushAsync(redisKey, redisValue);
        //}

        ///// <summary>
        ///// 返回列表上该键的长度，如果不存在，返回 0
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public async Task<long> ListLengthAsync(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.ListLengthAsync(redisKey);
        //}

        ///// <summary>
        ///// 返回在该列表上键所对应的元素
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public async Task<IEnumerable<RedisValue>> ListRangeAsync(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.ListRangeAsync(redisKey);
        //}

        ///// <summary>
        ///// 移除并返回存储在该键列表的第一个元素
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public async Task<T> ListLeftPopAsync<T>(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return Deserialize<T>(await _db.ListLeftPopAsync(redisKey));
        //}

        ///// <summary>
        ///// 移除并返回存储在该键列表的最后一个元素
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public async Task<T> ListRightPopAsync<T>(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return Deserialize<T>(await _db.ListRightPopAsync(redisKey));
        //}

        ///// <summary>
        ///// 在列表尾部插入值。如果键不存在，先创建再插入值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisValue"></param>
        ///// <returns></returns>
        //public async Task<long> ListRightPushAsync<T>(string redisKey, T redisValue)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.ListRightPushAsync(redisKey, Serialize(redisValue));
        //}

        ///// <summary>
        ///// 在列表头部插入值。如果键不存在，先创建再插入值
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisValue"></param>
        ///// <returns></returns>
        //public async Task<long> ListLeftPushAsync<T>(string redisKey, T redisValue)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.ListLeftPushAsync(redisKey, Serialize(redisValue));
        //}

        //#endregion List-async

        //#endregion List 操作

        //#region SortedSet 操作

        ///// <summary>
        ///// SortedSet 新增
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="member"></param>
        ///// <param name="score"></param>
        ///// <returns></returns>
        //public bool SortedSetAdd(string redisKey, string member, double score)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.SortedSetAdd(redisKey, member, score);
        //}

        ///// <summary>
        ///// 在有序集合中返回指定范围的元素，默认情况下从低到高。
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public IEnumerable<RedisValue> SortedSetRangeByRank(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.SortedSetRangeByRank(redisKey);
        //}

        ///// <summary>
        ///// 返回有序集合的元素个数
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public long SortedSetLength(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.SortedSetLength(redisKey);
        //}

        ///// <summary>
        ///// 返回有序集合的元素个数
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="memebr"></param>
        ///// <returns></returns>
        //public bool SortedSetLength(string redisKey, string memebr)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.SortedSetRemove(redisKey, memebr);
        //}

        ///// <summary>
        ///// SortedSet 新增
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="member"></param>
        ///// <param name="score"></param>
        ///// <returns></returns>
        //public bool SortedSetAdd<T>(string redisKey, T member, double score)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    var json = Serialize(member);

        //    return _db.SortedSetAdd(redisKey, json, score);
        //}

        //#region SortedSet-Async

        ///// <summary>
        ///// SortedSet 新增
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="member"></param>
        ///// <param name="score"></param>
        ///// <returns></returns>
        //public async Task<bool> SortedSetAddAsync(string redisKey, string member, double score)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.SortedSetAddAsync(redisKey, member, score);
        //}

        ///// <summary>
        ///// 在有序集合中返回指定范围的元素，默认情况下从低到高。
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public async Task<IEnumerable<RedisValue>> SortedSetRangeByRankAsync(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.SortedSetRangeByRankAsync(redisKey);
        //}

        ///// <summary>
        ///// 返回有序集合的元素个数
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public async Task<long> SortedSetLengthAsync(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.SortedSetLengthAsync(redisKey);
        //}

        ///// <summary>
        ///// 返回有序集合的元素个数
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="memebr"></param>
        ///// <returns></returns>
        //public async Task<bool> SortedSetRemoveAsync(string redisKey, string memebr)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.SortedSetRemoveAsync(redisKey, memebr);
        //}

        ///// <summary>
        ///// SortedSet 新增
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="member"></param>
        ///// <param name="score"></param>
        ///// <returns></returns>
        //public async Task<bool> SortedSetAddAsync<T>(string redisKey, T member, double score)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    var json = Serialize(member);

        //    return await _db.SortedSetAddAsync(redisKey, json, score);
        //}

        //#endregion SortedSet-Async

        //#endregion SortedSet 操作

        //#region key 操作

        ///// <summary>
        ///// 移除指定 Key
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public bool KeyDelete(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.KeyDelete(redisKey);
        //}

        ///// <summary>
        ///// 移除指定 Key
        ///// </summary>
        ///// <param name="redisKeys"></param>
        ///// <returns></returns>
        //public long KeyDelete(IEnumerable<string> redisKeys)
        //{
        //    var keys = redisKeys.Select(x => (RedisKey)AddKeyPrefix(x));
        //    return _db.KeyDelete(keys.ToArray());
        //}

        ///// <summary>
        ///// 校验 Key 是否存在
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public bool KeyExists(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.KeyExists(redisKey);
        //}

        ///// <summary>
        ///// 重命名 Key
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisNewKey"></param>
        ///// <returns></returns>
        //public bool KeyRename(string redisKey, string redisNewKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.KeyRename(redisKey, redisNewKey);
        //}

        ///// <summary>
        ///// 设置 Key 的时间
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="expiry"></param>
        ///// <returns></returns>
        //public bool KeyExpire(string redisKey, TimeSpan? expiry)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return _db.KeyExpire(redisKey, expiry);
        //}

        //#region key-async

        ///// <summary>
        ///// 移除指定 Key
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public async Task<bool> KeyDeleteAsync(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.KeyDeleteAsync(redisKey);
        //}

        ///// <summary>
        ///// 移除指定 Key
        ///// </summary>
        ///// <param name="redisKeys"></param>
        ///// <returns></returns>
        //public async Task<long> KeyDeleteAsync(IEnumerable<string> redisKeys)
        //{
        //    var keys = redisKeys.Select(x => (RedisKey)AddKeyPrefix(x));
        //    return await _db.KeyDeleteAsync(keys.ToArray());
        //}

        ///// <summary>
        ///// 校验 Key 是否存在
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <returns></returns>
        //public async Task<bool> KeyExistsAsync(string redisKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.KeyExistsAsync(redisKey);
        //}

        ///// <summary>
        ///// 重命名 Key
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="redisNewKey"></param>
        ///// <returns></returns>
        //public async Task<bool> KeyRenameAsync(string redisKey, string redisNewKey)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.KeyRenameAsync(redisKey, redisNewKey);
        //}

        ///// <summary>
        ///// 设置 Key 的时间
        ///// </summary>
        ///// <param name="redisKey"></param>
        ///// <param name="expiry"></param>
        ///// <returns></returns>
        //public async Task<bool> KeyExpireAsync(string redisKey, TimeSpan? expiry)
        //{
        //    redisKey = AddKeyPrefix(redisKey);
        //    return await _db.KeyExpireAsync(redisKey, expiry);
        //}

        //#endregion key-async

        //#endregion key 操作
    }
}
