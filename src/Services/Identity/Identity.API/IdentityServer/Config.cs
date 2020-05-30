using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace LifeCMS.Services.Identity.API.Authorization.IdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources(IConfigurationSection configurationSection)
        {
            return configurationSection
                    .GetChildren()
                    .Select(x => BuildApiResource(x));
        }

        public static IEnumerable<Client> GetClients(IConfigurationSection configurationSection)
        {
            return configurationSection
                    .GetChildren()
                    .Select(x => BuildClient(x));
        }

        private static Client BuildClient(IConfigurationSection configuration)
        {
            var c = new Client()
            {
                AllowedGrantTypes = GrantTypes.Code,

                AllowedCorsOrigins = GetArray(
                    configuration.GetSection("AllowedCorsOrigins")
                ),

                AllowedScopes = GetArray(
                    configuration.GetSection("AllowedScopes")
                ),

                ClientId = configuration.GetValue<string>("ClientId"),

                ClientName = configuration.GetValue<string>("ClientName"),

                PostLogoutRedirectUris = GetArray(
                    configuration.GetSection("PostLogoutRedirectUris")
                ),

                RequireConsent = configuration.GetValue<bool>("RequireConsent"),

                RequireClientSecret = configuration.GetValue<bool>("RequireClientSecret"),

                RedirectUris = GetArray(
                    configuration.GetSection("RedirectUris")
                ),
            };

            return c;
        }

        private static ApiResource BuildApiResource(IConfigurationSection configuration)
        {
            var scopes = configuration
                .GetSection("Scopes")
                .GetChildren()
                .Select(x => BuildScope(x))
                .ToArray();

            return new ApiResource()
            {
                Name = configuration.GetValue<string>("Name"),
                Scopes = scopes
            };
        }

        private static Scope BuildScope(IConfigurationSection configuration)
        {
            return new Scope(
                configuration.GetValue<string>("Name"),
                configuration.GetValue<string>("DisplayName")
            );
        }

        private static ICollection<string> GetArray(IConfigurationSection configuration)
        {
            return configuration
                .GetChildren()
                .Select(x => x.Value)
                .ToArray();
        }
    }
}