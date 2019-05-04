using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace AuthorizationCenter
{
    public class MyPersistedGrantStore : IPersistedGrantStore
    {
        //https://www.bbsmax.com/A/kvJ3raK7zg/
        //https://www.cnblogs.com/skig/p/AspNetCoreAuthCode.html
        //https://www.cnblogs.com/Leo_wl/p/6079515.html
        //https://blog.csdn.net/weixin_33971205/article/details/86031826
        //https://www.2cto.com/kf/201812/788836.html
        private readonly ConcurrentDictionary<string, PersistedGrant> _repository;

        readonly string _filePath = System.IO.Directory.GetCurrentDirectory() + "\\logs\\grantstore.txt";
        public MyPersistedGrantStore()
        {
            ConcurrentDictionary<string, PersistedGrant> dict = null;
            try
            {
                var vals = System.IO.File.ReadAllText(_filePath);
                if (!string.IsNullOrEmpty(vals))
                {
                    dict = Newtonsoft.Json.JsonConvert.DeserializeObject<ConcurrentDictionary<string, PersistedGrant>>(vals); 
                }
            }
            catch
            {
            }
            _repository = dict ?? new ConcurrentDictionary<string, PersistedGrant>();
        }

        void UpdateStore()
        {
            //这里做测试写入到文件中，如果是实际使用应该写入到数据库/NoSql(Redis)中
            System.IO.File.WriteAllText(_filePath, Newtonsoft.Json.JsonConvert.SerializeObject(_repository));
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            _repository[grant.Key] = grant;
            UpdateStore();
            return Task.FromResult(0);

            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    //移除防止重复
            //    await RemoveAsync(grant.Key);
            //    string sql = "insert into PersistedGrants([Key],ClientId,CreationTime,Data,Expiration,SubjectId,Type) values(@Key,@ClientId,@CreationTime,@Data,@Expiration,@SubjectId,@Type)";
            //    await connection.ExecuteAsync(sql, grant);
            //}
        }

        public Task<PersistedGrant> GetAsync(string key)
        {
            PersistedGrant token;
            if (_repository.TryGetValue(key, out token))
            {
                return Task.FromResult(token);
            }

            return Task.FromResult<PersistedGrant>(null);

            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    string sql = "select * from PersistedGrants where [Key]=@key";
            //    var result = await connection.QueryFirstOrDefaultAsync(sql, new { key });
            //    var model = result.ToModel();
            //    _logger.LogDebug("{persistedGrantKey} found in database: {persistedGrantKeyFound}", key, model != null);
            //    return model;
            //}
        }

        public Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            var query =
                from item in _repository
                where item.Value.SubjectId == subjectId
                select item.Value;

            var items = query.ToArray().AsEnumerable();
            return Task.FromResult(items);

            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    string sql = "select * from PersistedGrants where SubjectId=@subjectId";
            //    var data = (await connection.QueryAsync(sql, new { subjectId })).AsList();
            //    var model = data.Select(x => x.ToModel());
            //    _logger.LogDebug("{persistedGrantCount} persisted grants found for {subjectId}", data.Count, subjectId);
            //    return model;
            //}
        }

        public Task RemoveAsync(string key)
        {
            PersistedGrant val;
            _repository.TryRemove(key, out val);
            UpdateStore();
            return Task.FromResult(0);

            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    string sql = "delete from PersistedGrants where [Key]=@key";
            //    await connection.ExecuteAsync(sql, new { key });
            //    _logger.LogDebug("remove {key} from database success", key);
            //}
        }

        public Task RemoveAllAsync(string subjectId, string clientId)
        {
            var query =
                from item in _repository
                where item.Value.ClientId == clientId &&
                    item.Value.SubjectId == subjectId
                select item.Key;

            var keys = query.ToArray();
            foreach (var key in keys)
            {
                PersistedGrant grant;
                _repository.TryRemove(key, out grant);
            }
            UpdateStore();
            return Task.FromResult(0);

            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    string sql = "delete from PersistedGrants where ClientId=@clientId and SubjectId=@subjectId";
            //    await connection.ExecuteAsync(sql, new { subjectId, clientId });
            //    _logger.LogDebug("remove {subjectId} {clientId} from database success", subjectId, clientId);
            //}
        }

        public Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            var query =
                from item in _repository
                where item.Value.SubjectId == subjectId &&
                    item.Value.ClientId == clientId &&
                    item.Value.Type == type
                select item.Key;

            var keys = query.ToArray();
            foreach (var key in keys)
            {
                PersistedGrant grant;
                _repository.TryRemove(key, out grant);
            }
            UpdateStore();
            return Task.FromResult(0);

            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    string sql = "delete from PersistedGrants where ClientId=@clientId and SubjectId=@subjectId and Type=@type";
            //    await connection.ExecuteAsync(sql, new { subjectId, clientId });
            //    _logger.LogDebug("remove {subjectId} {clientId} {type} from database success", subjectId, clientId, type);
            //}
        }
    }
}
