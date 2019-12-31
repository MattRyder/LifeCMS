using System.Reflection;
using IdentityServer4.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Socialite.Infrastructure.Identity;

namespace Socialite.WebAPI.Startup
{
    public static partial class StartupExtensions
    {
        public static void AddSocialiteIdentityServer(
            this IServiceCollection services,
            IConfiguration configuration,
            string migrationsAssembly
        )
        {
            var identityDbConnectionString = configuration.GetConnectionString("SocialiteIdentity");

            services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                options.UserInteraction = new UserInteractionOptions
                {
                    LogoutUrl = "/Account/Logout",
                    LoginUrl = "/Account/Login",
                    LoginReturnUrlParameter = "returnUrl"
                };
            })
            .AddAspNetIdentity<SocialiteIdentityUser>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = db =>
                    db.UseMySql(
                        identityDbConnectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly)
                    );
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = db =>
                    db.UseMySql(
                        identityDbConnectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly)
                    );
            });
            // .AddInMemoryIdentityResources(Config.GetIdentityResources())
            // .AddInMemoryApiResources(Config.GetApis())
            // .AddInMemoryClients(Config.GetClients());
        }
    }
}