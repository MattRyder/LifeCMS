using System;
using MediatR;

namespace Socialite.WebAPI.Application.Commands.Posts
{
    public class PublishPostCommand : IRequest<bool>
    {
        public Guid PostId { get; private set; }

        public PublishPostCommand(Guid postId)
        {
            PostId = postId;
        }

    }
}