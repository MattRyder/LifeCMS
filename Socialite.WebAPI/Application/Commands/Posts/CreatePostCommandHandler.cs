using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Socialite.Domain.AggregateModels.PostAggregate;

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
            var post = new Post(request.Title, request.Text);

            var wtf = _postRepository.Add(post);

            return await _postRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}