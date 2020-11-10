using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.Domain.Exceptions;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using LifeCMS.EventBus.Common.Interfaces;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Posts
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, bool>
    {
        private readonly IPostRepository _postRepository;

        private readonly ILogger<CreatePostCommandHandler> _logger;

        private readonly IUserAccessor _userAccessor;

        private readonly IEventBus _eventBus;

        public CreatePostCommandHandler(
            IPostRepository postRepository,
            ILogger<CreatePostCommandHandler> logger,
            IUserAccessor userAccessor,
            IEventBus eventBus
        )
        {
            _postRepository = postRepository;

            _logger = logger;

            _userAccessor = userAccessor;

            _eventBus = eventBus;
        }

        public async Task<bool> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var post = new Post(_userAccessor.Id, request.Title, request.Text);

                _postRepository.Add(post);

                post.SetPublishedState();

                return await _postRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (PostDomainException ex)
            {
                _logger.LogError(ex, null);

                return false;
            }
        }
    }
}
