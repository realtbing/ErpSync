using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace AuthorizationCenter
{
    public class MyClientStore : IClientStore
    {
        //readonly Scope _scope;
        readonly Dictionary<string, Client> _clients;
        //public MyClientStore(Scope scope)
        public MyClientStore()
        {
            //_scope = scope;
            _clients = new Dictionary<string, Client>()
            {
                {
                    "product.api.service",
                    new Client
                    {
                        //ClientId = "client",
                        ClientId = "product.api.service",
                        //ClientName = "ClientName",
                        ClientName = "Product Api Service Client",
                        ClientSecrets =
                        {
                            new Secret("productsecret".Sha256())
                        },
                        AllowedGrantTypes = new string[] { GrantType.AuthorizationCode },
                        //AllowedGrantTypes = new string[] { GrantType.ResourceOwnerPassword, GrantType.ClientCredentials },
                        //AllowedGrantTypes = new string[] { GrantType.ResourceOwnerPassword, GrantType.ClientCredentials, GrantType.AuthorizationCode },

                        //RedirectUris = { "http://localhost:11000/Home/AuthCode" },
                        //PostLogoutRedirectUris = { "http://localhost:11000/" },

                        AccessTokenLifetime = 3600, //AccessToken过期时间， in seconds (defaults to 3600 seconds / 1 hour)
                        AuthorizationCodeLifetime = 600,  //设置AuthorizationCode的有效时间，in seconds (defaults to 300 seconds / 5 minutes)
                        AbsoluteRefreshTokenLifetime = 2592000,  //RefreshToken的最大过期时间，in seconds. Defaults to 2592000 seconds / 30 day
                        //AllowedScopes = (from l in _apiResource.Scopes select l.Name).ToList(),

                        AllowedScopes = { "clientservice", "productservice" }
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
