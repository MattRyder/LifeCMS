using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Socialite.Infrastructure.Data;
using Socialite.Infrastructure.Identity;
using Socialite.WebAPI.IdentityServer;

namespace Socialite.WebAPI.Startup
{
    public static partial class StartupExtensions
    {
        public static void SetupIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

            var signingKeyString = configuration.GetValue("JwtSigningKey", "");

            var signingKey = GetJwtSecurityKey(signingKeyString);

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            services
            .AddIdentity<SocialiteIdentityUser, SocialiteIdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<SocialiteIdentityDbContext>()
            .AddDefaultTokenProviders();

            services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.SaveToken = true;
            });
        }

        private static SecurityKey GetJwtSecurityKey(string signingKey)
        {
            if (string.IsNullOrEmpty(signingKey))
            {
                throw new System.Exception("No signing key present in application configuration.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signingKey));

            return securityKey;
        }
    }

}