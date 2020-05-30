using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.Events.Statuses;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.StatusAggregate;
using LifeCMS.Services.ContentCreation.Domain.Exceptions;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Statuses
{
    public class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand, BasicResponse>
    {
        private readonly IStatusRepository _statusRepository;

        private readonly ILogger<CreateStatusCommandHandler> _logger;

        private readonly IMediator _mediator;

        public CreateStatusCommandHandler(
            IStatusRepository statusRepository,
            ILogger<CreateStatusCommandHandler> logger,
            IMediator mediator)
        {
            _statusRepository = statusRepository;

            _logger = logger;

            _mediator = mediator;
        }

        public async Task<BasicResponse> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var status = new Status(request.Mood, request.Text);

                _statusRepository.Add(status);

                await _statusRepository.UnitOfWork.SaveEntitiesAsync();

                await _mediator.Publish(new StatusCreatedEvent(status));

                return new BasicResponse
                {
                    Success = true
                };
            }
            catch (StatusDomainException ex)
            {
                _logger.LogError(ex, null);

                return new BasicResponse
                {
                    Success = false,
                    Errors = new[] { ex.Message }
                };
            }
        }
    }
}