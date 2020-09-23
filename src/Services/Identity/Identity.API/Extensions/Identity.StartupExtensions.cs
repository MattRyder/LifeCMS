using System;
using LifeCMS.Services.Identity.Infrastructure;
using LifeCMS.Services.Identity.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LifeCMS.Services.Identity.API.Extensions
{
    public static partial class StartupExtensions
    {
        /// <summary>
        /// Add ASP.NET Core Identity to the application.
        /// </summary>
        public static void AddLifeCMSIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var identityDbConnectionString = configuration.GetConnectionString("LifeCMSIdentity");

            services
            .AddDbContext<LifeCMSIdentityDbContext>(options => options.UseMySql(identityDbConnectionString))
            .AddIdentity<LifeCMSIdentityUser, LifeCMSIdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<LifeCMSIdentityDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                var tokenLifespanHours = configuration.GetValue<int>(
                    "Identity:PasswordResetTokenLifetimeHours", 3
                );

                options.TokenLifespan = TimeSpan.FromHours(tokenLifespanHours);
            });
        }

        public static void UseLifeCMSIdentity(this IApplicationBuilder app)
        {
            app.ApplyMigrations<LifeCMSIdentityDbContext>();
        }
    }
}
