// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;

namespace AuthorizationCenter
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        // scopes define the resources in your system
        public static IEnumerable<ApiResource> GetResources()
        {
            return new List<ApiResource>
            {
                new ApiResource()
                {
                    Name = "Api",
                    DisplayName = "Api access"
                }
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    //ClientId = "client",
                    ClientId = "auth_clientid",
                    //ClientName = "ClientName",
                    ClientName = "AuthorizationCode Clientid",
                    AllowedGrantTypes = new string[] { GrantType.AuthorizationCode },

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { "http://localhost:11000/Home/AuthCode" },
                    PostLogoutRedirectUris = { "http://localhost:11000/" },

                    AccessTokenLifetime = 3600, //AccessToken过期时间， in seconds (defaults to 3600 seconds / 1 hour)
                        AuthorizationCodeLifetime = 600,  //设置AuthorizationCode的有效时间，in seconds (defaults to 300 seconds / 5 minutes)
                        AbsoluteRefreshTokenLifetime = 2592000,  //RefreshToken的最大过期时间，in seconds. Defaults to 2592000 seconds / 30 day
                        //AllowedScopes = (from l in _apiResource.Scopes select l.Name).ToList(),

                    AllowedScopes =
                    {
                        "Api"
                    }
                }
            };
        }
    }
}