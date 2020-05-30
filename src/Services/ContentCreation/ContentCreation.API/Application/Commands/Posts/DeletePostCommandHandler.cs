using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Application.Enums;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Posts
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, DeleteCommandResult>
    {
        private readonly IPostRepository _postRepository;

        public DeletePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<DeleteCommandResult> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.FindAsync(request.PostId);

            if (post == null)
            {
                return DeleteCommandResult.NotFound;
            }

            _postRepository.Delete(post);

            var result = await _postRepository.UnitOfWork.SaveEntitiesAsync();

            return result ? DeleteCommandResult.Success : DeleteCommandResult.Failure;
        }
    }
}
