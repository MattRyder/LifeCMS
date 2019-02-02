using MediatR;
using Socialite.Domain.AggregateModels.StatusAggregate;

namespace Socialite.WebAPI.Application.Commands
{
    public class CreateStatusCommand : IRequest<bool>
    {
        public Status Status { get; private set; }

        public CreateStatusCommand(Status status)
        {
            Status = status;
        }
    }
}