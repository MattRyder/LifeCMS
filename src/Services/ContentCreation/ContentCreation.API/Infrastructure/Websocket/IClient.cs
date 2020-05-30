using System.Threading.Tasks;

namespace LifeCMS.Services.ContentCreation.API.Infrastructure.Websocket
{
    public interface IClient
    {
        Task RecieveMessage(string user, string message);
    }
}