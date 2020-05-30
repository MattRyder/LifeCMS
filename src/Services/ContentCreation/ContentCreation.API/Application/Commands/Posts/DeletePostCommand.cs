using System;
using MediatR;
using LifeCMS.Services.ContentCreation.API.Application.Enums;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Posts
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
