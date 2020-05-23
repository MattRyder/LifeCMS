using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Socialite.WebAPI.Infrastructure.Websocket;

namespace Socialite.WebAPI.Startup
{
    public static partial class StartupExtensions
    {
        public static void RegisterSignalR(this IServiceCollection services)
        {
            services.AddSignalR();
        }

        public static void MapSignalrHubs(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<WebsocketClient>("/api/service/websocket");
        }
    }
}