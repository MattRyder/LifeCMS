using System;
using MediatR;
using Socialite.WebAPI.Application.Enums;

namespace Socialite.WebAPI.Application.Commands.Posts
{
    public class DeletePostCommand : IRequest<DeleteCommandResult>
    {
        public Guid PostId { get; private set; }

        public DeletePostCommand(Guid postId)
        {
            PostId = postId;
        }
    }

}