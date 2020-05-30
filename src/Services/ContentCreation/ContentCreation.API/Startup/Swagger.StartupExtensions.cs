using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LifeCMS.Services.ContentCreation.API.Startup
{
    public static partial class StartupExtensions
    {
        public static void AddLifeCMSSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "1", Title = "LifeCMS API" });
            });
        }

        public static void UseLifeCMSSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LifeCMS API v1");
            });
        }
    }
}
