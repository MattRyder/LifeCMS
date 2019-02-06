using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Infrastructure.Repositories;

namespace Socialite.WebAPI.Application.Commands.Status
{
    class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand, DeleteStatusCommandResult>
    {
        private readonly IStatusRepository _statusRepository;

        public DeleteStatusCommandHandler(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<DeleteStatusCommandResult> Handle(DeleteStatusCommand command, CancellationToken cancellationToken)
        {
            var status = await _statusRepository.FindAsync(command.StatusId);

            if (status != null)
            {
                _statusRepository.Delete(status);

                var result = await _statusRepository.UnitOfWork.SaveEntitiesAsync();

                return result ? DeleteStatusCommandResult.Success : DeleteStatusCommandResult.Failure;

            }
            else
            {
                return DeleteStatusCommandResult.NotFound;
            }
        }
    }
}