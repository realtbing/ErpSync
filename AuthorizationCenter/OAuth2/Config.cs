// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace AuthorizationCenter
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("clientservice", "CAS Client Service"),
                new ApiResource("productservice", "CAS Product Service"),
                new ApiResource("agentservice", "CAS Agent Service")
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
                    ClientId = "client.api.service",
                    //ClientName = "ClientName",
                    ClientName = "Client Api Service Client",
                    ClientSecrets =
                    {
                        new Secret("clientsecret".Sha256())
                    },
                    AllowedGrantTypes = new string[] { GrantType.ResourceOwnerPassword, GrantType.ClientCredentials, GrantType.AuthorizationCode },

                    //RedirectUris = { "http://localhost:11000/Home/AuthCode" },
                    //PostLogoutRedirectUris = { "http://localhost:11000/" },

                    AccessTokenLifetime = 3600, //AccessToken过期时间， in seconds (defaults to 3600 seconds / 1 hour)
                    AuthorizationCodeLifetime = 600,  //设置AuthorizationCode的有效时间，in seconds (defaults to 300 seconds / 5 minutes)
                    AbsoluteRefreshTokenLifetime = 2592000,  //RefreshToken的最大过期时间，in seconds. Defaults to 2592000 seconds / 30 day
                    //AllowedScopes = (from l in _apiResource.Scopes select l.Name).ToList(),

                    AllowedScopes = { "clientservice" }
                },
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
                    AllowedGrantTypes = new string[] { GrantType.ResourceOwnerPassword, GrantType.ClientCredentials, GrantType.AuthorizationCode },

                    //RedirectUris = { "http://localhost:11000/Home/AuthCode" },
                    //PostLogoutRedirectUris = { "http://localhost:11000/" },

                    AccessTokenLifetime = 3600, //AccessToken过期时间， in seconds (defaults to 3600 seconds / 1 hour)
                    AuthorizationCodeLifetime = 600,  //设置AuthorizationCode的有效时间，in seconds (defaults to 300 seconds / 5 minutes)
                    AbsoluteRefreshTokenLifetime = 2592000,  //RefreshToken的最大过期时间，in seconds. Defaults to 2592000 seconds / 30 day
                    //AllowedScopes = (from l in _apiResource.Scopes select l.Name).ToList(),

                    AllowedScopes = { "clientservice", "productservice" }
                },
                new Client
                {
                    //ClientId = "client",
                    ClientId = "agent.api.service",
                    //ClientName = "ClientName",
                    ClientName = "Agent Api Service Client",
                    ClientSecrets =
                    {
                        new Secret("agentsecret".Sha256())
                    },
                    AllowedGrantTypes = new string[] { GrantType.ResourceOwnerPassword, GrantType.ClientCredentials, GrantType.AuthorizationCode },

                    //RedirectUris = { "http://localhost:11000/Home/AuthCode" },
                    //PostLogoutRedirectUris = { "http://localhost:11000/" },

                    AccessTokenLifetime = 3600, //AccessToken过期时间， in seconds (defaults to 3600 seconds / 1 hour)
                    AuthorizationCodeLifetime = 600,  //设置AuthorizationCode的有效时间，in seconds (defaults to 300 seconds / 5 minutes)
                    AbsoluteRefreshTokenLifetime = 2592000,  //RefreshToken的最大过期时间，in seconds. Defaults to 2592000 seconds / 30 day
                    //AllowedScopes = (from l in _apiResource.Scopes select l.Name).ToList(),
                    
                    AllowedScopes = { "clientservice", "productservice", "agentservice" }
                }
            };
        }

        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<TestUser> GetUsers()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "10001",
                    Username = "edison@hotmail.com",
                    Password = "edisonpassword"
                },
                new TestUser
                {
                    SubjectId = "10002",
                    Username = "andy@hotmail.com",
                    Password = "andypassword"
                },
                new TestUser
                {
                    SubjectId = "10003",
                    Username = "leo@hotmail.com",
                    Password = "leopassword"
                }
            };
        }
    }
}