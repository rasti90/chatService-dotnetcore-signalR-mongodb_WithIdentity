// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.API
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
             new List<IdentityResource>
             {
               
             };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("ChatAPI", "Chat Service")
            };

        public static IEnumerable<ApiResource> ApiResourses =>
            new List<ApiResource>()
            {

            };

        public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "TestClientApplication",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("D369EE97CDC040C99D5E2C1998E44B9F".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "ChatAPI" }
            },
           
        };
    }
}