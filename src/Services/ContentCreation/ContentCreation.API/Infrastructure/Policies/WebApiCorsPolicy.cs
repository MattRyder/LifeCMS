using Microsoft.AspNetCore.Cors.Infrastructure;

namespace LifeCMS.Services.ContentCreation.API.Infrastructure.Policies
{
    public class WebApiCorsPolicy
    {
        public static string GetName()
        {
            return "WebAPICorsPolicy";
        }

        public static CorsPolicy GetPolicy()
        {
            return new CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .Build();
        }
    }
}