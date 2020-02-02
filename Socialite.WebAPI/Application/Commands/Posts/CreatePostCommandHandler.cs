using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.Exceptions;

namespace Socialite.WebAPI.Application.Commands.Posts
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, bool>
    {
        private readonly IPostRepository _postRepository;

        public CreatePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<bool> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var post = new Post(request.AuthorId, request.Title, request.Text);

                var wtf = _postRepository.Add(post);

                return await _postRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch(PostDomainException)
            {
                return false;
            }
        }
    }
}