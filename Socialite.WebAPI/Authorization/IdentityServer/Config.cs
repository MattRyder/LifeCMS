using System.Collections.Generic;
using IdentityServer4.Models;

namespace Socialite.WebAPI.Authorization.IdentityServer
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
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes = { "status:read" },

                    ClientId = "SocialiteClient",

                    ClientSecrets =
                    {
                        new Secret("SocialiteClientSecret".Sha512())
                    },

                    RedirectUris =
                    {
                        "http://127.0.0.1:5000/sessions/callback"
                    }
                }
            };
        }
    }
}