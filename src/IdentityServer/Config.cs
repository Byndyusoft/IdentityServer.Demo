// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("user_profile", "My Profile"),
                new ApiScope("authentication", "Identity Provider Auth"),
                new ApiScope("message", "My Messages"),
                new ApiScope("photo", "My Photos")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("Api", "My API")
                {
                    Scopes = new[] {"user_profile", "authentication", "message", "photo"}
                }
            };
    }
}