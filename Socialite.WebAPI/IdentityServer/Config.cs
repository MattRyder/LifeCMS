using System.Collections.Generic;
using IdentityServer4.Models;

namespace Socialite.WebAPI.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("status:read", "Read access to Status")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client()
                {
                    AllowedGrantTypes = GrantTypes.Code,

                    AllowedScopes = { "openid", "status:read" },

                    ClientId = "SocialiteClient",

                    ClientSecrets =
                    {
                        new Secret("SocialiteClientSecret".Sha512())
                    },

                    RedirectUris =
                    {
                        "https://127.0.0.1:3000/sessions/callback"
                    }
                }
            };
        }
    }
}