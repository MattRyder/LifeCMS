using MediatR;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.WebAPI.Application.Enums;

namespace Socialite.WebAPI.Application.Commands.Statuses
{
    public class DeleteStatusCommand : IRequest<DeleteCommandResult>
    {
        public int StatusId { get; private set; }

        public DeleteStatusCommand(int statusId)
        {
            StatusId = statusId;
        }
    }
}