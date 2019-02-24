using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Socialite.Domain.AggregateModels.PostAggregate;

namespace Socialite.WebAPI.Application.Commands.Posts
{
    public class PublishPostCommandHandler : IRequestHandler<PublishPostCommand, bool>
    {
        private readonly IPostRepository _postRepository;

        public PublishPostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<bool> Handle(PublishPostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.FindAsync(request.PostId);

            if(post == null)
            {
                return false;
            }

            post.SetPublishedState();

            return await _postRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}