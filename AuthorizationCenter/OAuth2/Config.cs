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
                    ClientId = "client",
                    ClientName = "ClientName",
                    AllowedGrantTypes = new string[] { GrantType.AuthorizationCode },

                    ClientSecrets = 
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { "http://localhost:11000/" },
                    PostLogoutRedirectUris = { "http://localhost:11000/" },

                    AuthorizationCodeLifetime = 600,

                    AllowedScopes = 
                    {
                        "Api"
                    }
                }
            };
        }
    }
}