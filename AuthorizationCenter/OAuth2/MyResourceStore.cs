using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace AuthorizationCenter
{
    public class MyResourceStore : IResourceStore
    {
        //http://www.mamicode.com/info-detail-2178116.html
        readonly static Dictionary<string, ApiResource> _resources = new Dictionary<string, ApiResource>()
        {
            {
                "Api",
                new ApiResource
                {
                    Name = "Api",
                    DisplayName = "Api",
                    Description = "My API",
                    UserClaims = new List<string> {"Role"},
                    ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
                }
            }
        };
        readonly static Dictionary<string, IdentityResource> _idResources = new Dictionary<string, IdentityResource>()
        {
            {
                "Api",
                new IdentityResource
                {
                    Name = "Api",
                    DisplayName = "Api",
                    Description = "My API",
                    UserClaims = new List<string> {"Role"}
                }
            }
        };

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            ApiResource ar;
            if (_resources.TryGetValue(name, out ar))
            {
                return Task.FromResult<ApiResource>(ar);
            }
            else
            {
                return Task.FromResult<ApiResource>(_resources.GetValueOrDefault("Api"));
            }

            //var model = new ApiResource();
            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    string sql = @"select * from ApiResources where Name=@Name and Enabled=1;
            //                   select * from ApiResources t1 inner join ApiScopes t2 on t1.Id=t2.ApiResourceId where t1.Name=@name and Enabled=1;";

            //    var multi = await connection.QueryMultipleAsync(sql, new { name });
            //    var ApiResources = multi.Read();
            //    var ApiScopes = multi.Read();

            //    if (ApiResources != null && ApiResources.AsList().Count > 0)
            //    {
            //        var apiresource = ApiResources.AsList()[0];
            //        apiresource.Scopes = ApiScopes.AsList();
            //        if (apiresource != null)
            //        {
            //            _logger.LogDebug("Found {api} API resource in database", name);
            //        }
            //        else
            //        {
            //            _logger.LogDebug("Did not find {api} API resource in database", name);
            //        }
            //        model = apiresource.ToModel();
            //    }
            //}
            //return model;
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            List<ApiResource> resources = new List<ApiResource>();
            if (scopeNames != null)
            {
                ApiResource ar;
                foreach (var name in scopeNames)
                {
                    if (_resources.TryGetValue(name, out ar))
                    {
                        resources.Add(ar);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //返回值scopes不能为null
            return Task.FromResult<IEnumerable<ApiResource>>(resources);

            //var apiResourceData = new List();
            //string _scopes = "";
            //foreach (var scope in scopeNames)
            //{
            //    _scopes += "'" + scope + "',";
            //}

            //if (_scopes == "")
            //{
            //    return null;
            //}
            //else
            //{
            //    _scopes = _scopes.Substring(0, _scopes.Length - 1);
            //}

            //string sql = "select distinct t1.* from ApiResources t1 inner join ApiScopes t2 on t1.Id=t2.ApiResourceId where t2.Name in(" + _scopes + ") and Enabled=1;";
            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    var apir = (await connection.QueryAsync(sql)).AsList();
            //    if (apir != null && apir.Count > 0)
            //    {
            //        foreach (var apimodel in apir)
            //        {
            //            sql = "select * from ApiScopes where ApiResourceId=@id";
            //            var scopedata = (await connection.QueryAsync(sql, new { id = apimodel.Id })).AsList();
            //            apimodel.Scopes = scopedata;
            //            apiResourceData.Add(apimodel.ToModel());
            //        }
            //        _logger.LogDebug("Found {scopes} API scopes in database", apiResourceData.SelectMany(x => x.Scopes).Select(x => x.Name));
            //    }
            //}
            //return apiResourceData;
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            List<IdentityResource> resources = new List<IdentityResource>();
            if (scopeNames != null)
            {
                IdentityResource ar;
                foreach (var name in scopeNames)
                {
                    if (_idResources.TryGetValue(name, out ar))
                    {
                        resources.Add(ar);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //返回值scopes不能为null
            return Task.FromResult<IEnumerable<IdentityResource>>(resources);

            //var apiResourceData = new List();
            //string _scopes = "";
            //foreach (var scope in scopeNames)
            //{
            //    _scopes += "'" + scope + "',";
            //}

            //if (_scopes == "")
            //{
            //    return null;
            //}
            //else
            //{
            //    _scopes = _scopes.Substring(0, _scopes.Length - 1);
            //}

            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    //暂不实现 IdentityClaims
            //    string sql = "select * from IdentityResources where Enabled=1 and Name in(" + _scopes + ")";
            //    var data = (await connection.QueryAsync(sql)).AsList();
            //    if (data != null && data.Count > 0)
            //    {
            //        foreach (var model in data)
            //        {
            //            apiResourceData.Add(model.ToModel());
            //        }
            //    }
            //}
            //return apiResourceData;
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            var identityResourceData = new List<IdentityResource>();
            var apiResourceData = new List<ApiResource>();

            foreach (var key in _idResources.Keys)
            {
                identityResourceData.Add(_idResources[key]);
            }

            foreach (var key in _resources.Keys)
            {
                apiResourceData.Add(_resources[key]);
            }

            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    string sql = "select * from IdentityResources where Enabled=1";
            //    var data = (await connection.QueryAsync(sql)).AsList();
            //    if (data != null && data.Count > 0)
            //    {
            //        foreach (var m in data)
            //        {
            //            identityResourceData.Add(m.ToModel());
            //        }
            //    }

            //    //获取apiresource
            //    sql = "select * from ApiResources where Enabled=1";
            //    var apidata = (await connection.QueryAsync(sql)).AsList();
            //    if (apidata != null && apidata.Count > 0)
            //    {
            //        foreach (var m in apidata)
            //        {
            //            sql = "select * from ApiScopes where ApiResourceId=@id";
            //            var scopedata = (await connection.QueryAsync(sql, new { id = m.Id })).AsList();
            //            m.Scopes = scopedata;
            //            apiResourceData.Add(m.ToModel());
            //        }
            //    }
            //}

            return Task.FromResult<Resources>(new Resources(identityResourceData, apiResourceData));
        }
    }
}
