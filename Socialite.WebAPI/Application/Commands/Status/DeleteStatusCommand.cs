using MediatR;
using Socialite.Domain.AggregateModels.StatusAggregate;

namespace Socialite.WebAPI.Application.Commands.Status
{
    enum DeleteStatusCommandResult
    {
        // Successfully deleted the Status
        Success,

        // Failed to find the Status
        NotFound,

        // Failed to delete the Status
        Failure
    }

    public class DeleteStatusCommand : IRequest<DeleteStatusCommandResult>
    {
        public int StatusId { get; private set; }

        public DeleteStatusCommand(int statusId)
        {
            StatusId = statusId;
        }
    }
}