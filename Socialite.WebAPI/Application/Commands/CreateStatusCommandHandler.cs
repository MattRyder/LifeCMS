using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Infrastructure.Repositories;

namespace Socialite.WebAPI.Application.Commands
{
    class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand, bool>
    {
        private readonly StatusRepository _statusRepository;

        public CreateStatusCommandHandler(StatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<bool> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            _statusRepository.Add(request.Status);

            return await _statusRepository.StatusContext.SaveEntitiesAsync();
        }
    }
}