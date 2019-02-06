using MediatR;
using Socialite.Domain.AggregateModels.StatusAggregate;

namespace Socialite.WebAPI.Application.Commands.Status
{
    public class CreateStatusCommand : IRequest<bool>
    {
        public Domain.AggregateModels.StatusAggregate.Status Status { get; private set; }

        public CreateStatusCommand(Domain.AggregateModels.StatusAggregate.Status status)
        {
            Status = status;
        }
    }
}