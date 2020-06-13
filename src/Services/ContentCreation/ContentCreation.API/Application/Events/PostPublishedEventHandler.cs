using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Websocket;
using LifeCMS.Services.ContentCreation.Domain.Events.Posts;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace LifeCMS.Services.ContentCreation.API.Application.Events
{
    public class PostPublishedEventHandler : INotificationHandler<PostPublishedEvent>
    {
        private readonly IHubContext<ContentCreationClient, IContentCreationClient> _websocketClient;

        public PostPublishedEventHandler(IHubContext<ContentCreationClient, IContentCreationClient> websocketClient)
        {
            _websocketClient = websocketClient;
        }

        public Task Handle(PostPublishedEvent notification, CancellationToken cancellationToken)
        {
            return _websocketClient.Clients.All.PostPublished(
                notification.Post.AuthorId.ToString(),
                notification.Post.Id.ToString()
            );
        }
    }
}