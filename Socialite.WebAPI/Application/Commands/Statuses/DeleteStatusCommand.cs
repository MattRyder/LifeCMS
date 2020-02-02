using System;
using MediatR;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.WebAPI.Application.Enums;

namespace Socialite.WebAPI.Application.Commands.Statuses
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