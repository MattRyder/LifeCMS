using System;
using Microsoft.Extensions.Configuration;

namespace LifeCMS.Services.ContentCreation.API.Startup
{
    public static class ConfigurationHelper
    {
        public static string GetConnectionString(
            IConfiguration configuration,
            string connectionStringName
        )
        {
            var connectionString = configuration.GetConnectionString(
                connectionStringName
            );

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException($"The connection string `{connectionStringName}` was not provided.");
            }

            return connectionString;
        }

        public static string GetConfigurationValue(
            IConfiguration configuration,
            string key)
        {
            var value = configuration[key];

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(
                    $"The environment variable `{key}` was not provided.");
            }

            return value;
        }
    }
}
