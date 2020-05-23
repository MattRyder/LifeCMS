using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Domain.Events.Statuses;
using Socialite.Domain.Exceptions;
using Socialite.Infrastructure.Responses;

namespace Socialite.WebAPI.Application.Commands.Statuses
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