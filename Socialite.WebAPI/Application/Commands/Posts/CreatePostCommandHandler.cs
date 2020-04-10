using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.Exceptions;

namespace Socialite.WebAPI.Application.Commands.Posts
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, bool>
    {
        private readonly IPostRepository _postRepository;

        private readonly ILogger<CreatePostCommandHandler> _logger;

        public CreatePostCommandHandler(IPostRepository postRepository, ILogger<CreatePostCommandHandler> logger)
        {
            _postRepository = postRepository;

            _logger = logger;
        }

        public async Task<bool> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var post = new Post(request.AuthorId, request.Title, request.Text);

                _postRepository.Add(post);

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