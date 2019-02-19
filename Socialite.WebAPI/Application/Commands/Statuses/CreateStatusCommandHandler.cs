using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Infrastructure.Repositories;

namespace Socialite.WebAPI.Application.Commands.Statuses
{
    public class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand, bool>
    {
        private readonly IStatusRepository _statusRepository;

        public CreateStatusCommandHandler(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<bool> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            var status = new Status(request.Mood, request.Text);

            _statusRepository.Add(status);

            return await _statusRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}