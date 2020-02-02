using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Socialite.Infrastructure.Data;
using Socialite.Infrastructure.Identity;

namespace Socialite.Authentication.Extensions
{
    public static partial class StartupExtensions
    {
        /// <summary>
        /// Add ASP.NET Core Identity to the application.
        /// </summary>
        public static void AddSocialiteIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var identityDbConnectionString = configuration.GetConnectionString("SocialiteIdentity");

            services
            .AddDbContext<SocialiteIdentityDbContext>(options => options.UseMySql(identityDbConnectionString))
            .AddIdentity<SocialiteIdentityUser, SocialiteIdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<SocialiteIdentityDbContext>()
            .AddDefaultTokenProviders();
        }

        public static void UseSocialiteIdentity(this IApplicationBuilder app)
        {
            app.ApplyMigrations<SocialiteIdentityDbContext>();
        }
    }

}