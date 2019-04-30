using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace AuthorizationCenter
{
    public class MyClientStore : IClientStore
    {
        readonly Dictionary<string, Client> _clients;
        readonly IScopeStore _scopes;
        public MyClientStore(IScopeStore scopes)
        {
            _scopes = scopes;
            _clients = new Dictionary<string, Client>()
            {
                {
                    "auth_clientid",
                    new Client
                    {
                        ClientId = "auth_clientid",
                        ClientName = "AuthorizationCode Clientid",
                        AllowedGrantTypes = new string[] { GrantType.AuthorizationCode },
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        RedirectUris = { "http://localhost:6321/Home/AuthCode" },
                        PostLogoutRedirectUris = { "http://localhost:6321/" },
                        //AccessTokenLifetime = 3600, //AccessToken过期时间， in seconds (defaults to 3600 seconds / 1 hour)
                        //AuthorizationCodeLifetime = 300,  //设置AuthorizationCode的有效时间，in seconds (defaults to 300 seconds / 5 minutes)
                        //AbsoluteRefreshTokenLifetime = 2592000,  //RefreshToken的最大过期时间，in seconds. Defaults to 2592000 seconds / 30 day
                        AllowedScopes = (from l in _scopes.GetEnabledScopesAsync(true).Result select l.Name).ToList(),
                    }
                }
            };
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            Client client;
            _clients.TryGetValue(clientId, out client);
            return Task.FromResult(client);
        }
    }
}
