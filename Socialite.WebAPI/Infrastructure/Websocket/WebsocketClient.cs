using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Socialite.WebAPI.Infrastructure.Websocket
{
    public class WebsocketClient : Hub<IClient>
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.RecieveMessage(user, message);
        }

        public override async Task OnConnectedAsync()
        {
            System.Diagnostics.Debug.WriteLine($"Connected websocket: {Context.ConnectionId}");
            
            await base.OnConnectedAsync();
        }
    }
}