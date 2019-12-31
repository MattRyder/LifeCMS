using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Socialite.WebAPI.Startup
{
    public static partial class StartupExtensions
    {
        public static void SetupSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "1", Title = "Socialite API" });
            });

        }
    }
}