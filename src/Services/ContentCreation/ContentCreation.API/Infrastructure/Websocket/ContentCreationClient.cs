using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace LifeCMS.Services.ContentCreation.API.Infrastructure.Websocket
{
    public class ContentCreationClient : Hub<IContentCreationClient>
    {
        public async Task PostPublished(string user, string message)
        {
            await Clients.All.PostPublished(user, message);
        }

        public override async Task OnConnectedAsync()
        {
            System.Diagnostics.Debug.WriteLine($"Connected websocket: {Context.ConnectionId}");
            
            await base.OnConnectedAsync();
        }
    }
}