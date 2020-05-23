using System.Threading.Tasks;

namespace Socialite.WebAPI.Infrastructure.Websocket
{
    public interface IClient
    {
        Task RecieveMessage(string user, string message);
    }
}