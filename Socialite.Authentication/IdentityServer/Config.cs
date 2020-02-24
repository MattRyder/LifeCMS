using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using IdentityServer4.Models;

namespace Socialite.Authentication.Authorization.IdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource()
                {
                    Name = "SocialiteWebApi",

                    Scopes = new[]
                    {
                        new Scope("status:read", "Read access to the Status resource")
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client()
                {
                    ClientId = "SocialiteWebApiClient",

                    ClientName = "Socialite",

                    RequireConsent = false,

                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Code,

                    AllowedScopes =
                    {
                        "status:read"
                    },

                    AllowedCorsOrigins =
                    {
                        "http://localhost:3000"
                    },

                    RedirectUris =
                    {
                        "http://localhost:3000/session/oauth_callback"
                    },

                    PostLogoutRedirectUris = { "http://localhost:3000/signout-callback-oidc/" },
                }
            };
        }
    }
}