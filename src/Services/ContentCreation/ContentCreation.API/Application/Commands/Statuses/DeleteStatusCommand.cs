using System;
using LifeCMS.Services.ContentCreation.API.Application.Enums;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Statuses
{
    public class DeleteStatusCommand : IRequest<DeleteCommandResult>
    {
        public Guid StatusId { get; private set; }

        public DeleteStatusCommand(Guid statusId)
        {
            StatusId = statusId;
        }
    }
}
