using System.Threading.Tasks;

namespace LifeCMS.Services.ContentCreation.API.Infrastructure.Websocket
{
    public interface IContentCreationClient
    {
        Task PostPublished(string user, string message);
    }
}