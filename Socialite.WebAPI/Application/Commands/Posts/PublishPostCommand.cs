using MediatR;

namespace Socialite.WebAPI.Application.Commands.Posts
{
    public class PublishPostCommand : IRequest<bool>
    {
        public int PostId { get; private set; }

        public PublishPostCommand(int postId)
        {
            PostId = postId;
        }

    }
}