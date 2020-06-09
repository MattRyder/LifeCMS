using Microsoft.AspNetCore.Cors.Infrastructure;

namespace LifeCMS.Services.ContentCreation.API.Infrastructure.Policies
{
    public class WebsocketCorsPolicy
    {
        public static string GetName()
        {
            return "WebsocketCorsPolicy";
        }

        public static CorsPolicy GetPolicy(string[] allowedOrigins)
        {
            return new CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins(allowedOrigins)
                    .Build();
        }
    }
}