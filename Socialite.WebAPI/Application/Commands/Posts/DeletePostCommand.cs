using MediatR;
using Socialite.WebAPI.Application.Enums;

namespace Socialite.WebAPI.Application.Commands.Posts
{
    public class DeletePostCommand : IRequest<DeleteCommandResult>
    {
        public int PostId { get; private set; }

        public DeletePostCommand(int postId)
        {
            this.PostId = postId;
        }
    }
}