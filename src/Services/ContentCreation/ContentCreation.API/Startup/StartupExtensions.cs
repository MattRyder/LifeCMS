using System;
using Microsoft.Extensions.Configuration;

namespace LifeCMS.Services.ContentCreation.API.Startup
{
    public static partial class StartupExtensions
    {
        private static string GetConnectionString(
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
    }
}
