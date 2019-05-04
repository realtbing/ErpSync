using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace AuthorizationCenter
{
    public class MyScopeStore : IScopeStore
    {
        readonly static Dictionary<string, Scope> _scopes = new Dictionary<string, Scope>()
        {
            {
                "api1",
                new Scope
                {
                    Name = "api1",
                    DisplayName = "api1",
                    Description = "My API",
                }
            },
            {
                //RefreshToken的Scope
                StandardScopes.OfflineAccess.Name,
                StandardScopes.OfflineAccess
            },
        };

        public Task<IEnumerable<Scope>> FindScopesAsync(IEnumerable<string> scopeNames)
        {
            List<Scope> scopes = new List<Scope>();
            if (scopeNames != null)
            {
                Scope sc;
                foreach (var sname in scopeNames)
                {
                    if (_scopes.TryGetValue(sname, out sc))
                    {
                        scopes.Add(sc);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //返回值scopes不能为null
            return Task.FromResult<IEnumerable<Scope>>(scopes);
        }

        public Task<IEnumerable<Scope>> GetScopesAsync(bool publicOnly = true)
        {
            //publicOnly为true：获取public的scope；为false：获取所有的scope
            //这里不做区分
            return Task.FromResult<IEnumerable<Scope>>(_scopes.Values);
        }
    }
}
