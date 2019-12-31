using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Socialite.Infrastructure.Data;

namespace Socialite.WebAPI.Startup
{
    public static partial class StartupExtensions
    {
        public static void SetupEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Socialite");

            var identityDbConnectionString = configuration.GetConnectionString("SocialiteIdentity");

            services
            .AddEntityFrameworkSqlServer()
            .AddDbContext<SocialiteDbContext>(opts => opts.UseMySql(connectionString))
            .AddDbContext<SocialiteIdentityDbContext>(options => options.UseMySql(identityDbConnectionString));

            services.AddTransient<IDbConnectionFactory, MySqlDbConnectionFactory>(f =>
            {
                return new MySqlDbConnectionFactory(connectionString);
            });
        }
    }
}