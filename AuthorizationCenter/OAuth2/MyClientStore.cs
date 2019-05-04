﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace AuthorizationCenter
{
    public class MyClientStore : IClientStore
    {
        readonly Dictionary<string, Client> _clients;
        readonly ApiResource _apiResource;
        public MyClientStore(ApiResource apiResource)
        {
            _apiResource = apiResource;
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
                        RedirectUris = { "http://192.168.0.128:11000/Home/AuthCode" },
                        PostLogoutRedirectUris = { "http://localhost:11000/" },
                        //AccessTokenLifetime = 3600, //AccessToken过期时间， in seconds (defaults to 3600 seconds / 1 hour)
                        //AuthorizationCodeLifetime = 300,  //设置AuthorizationCode的有效时间，in seconds (defaults to 300 seconds / 5 minutes)
                        //AbsoluteRefreshTokenLifetime = 2592000,  //RefreshToken的最大过期时间，in seconds. Defaults to 2592000 seconds / 30 day
                        AllowedScopes = (from l in _apiResource.Scopes select l.Name).ToList(),
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
