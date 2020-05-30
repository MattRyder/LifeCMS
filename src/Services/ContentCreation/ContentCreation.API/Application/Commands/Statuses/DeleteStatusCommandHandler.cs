using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Statuses;
using LifeCMS.Services.ContentCreation.API.Application.Enums;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.StatusAggregate;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Statuses
{
    public class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand, DeleteCommandResult>
    {
        private readonly IStatusRepository _statusRepository;

        public DeleteStatusCommandHandler(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<DeleteCommandResult> Handle(DeleteStatusCommand command, CancellationToken cancellationToken)
        {
            var status = await _statusRepository.FindAsync(command.StatusId);

            if (status != null)
            {
                _statusRepository.Delete(status);

                var result = await _statusRepository.UnitOfWork.SaveEntitiesAsync();

                return result ? DeleteCommandResult.Success : DeleteCommandResult.Failure;
            }
            else
            {
                return DeleteCommandResult.NotFound;
            }
        }
    }
}
