using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Domain.Exceptions;
using Socialite.Infrastructure.Responses;

namespace Socialite.WebAPI.Application.Commands.Statuses
{
    public class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand, BasicResponse>
    {
        private readonly IStatusRepository _statusRepository;

        private readonly ILogger<CreateStatusCommandHandler> _logger;

        public CreateStatusCommandHandler(IStatusRepository statusRepository, ILogger<CreateStatusCommandHandler> logger)
        {
            _statusRepository = statusRepository;

            _logger = logger;
        }

        public async Task<BasicResponse> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var status = new Status(request.Mood, request.Text);

                _statusRepository.Add(status);

                var result = await _statusRepository.UnitOfWork.SaveEntitiesAsync();

                return new BasicResponse
                {
                    Success = result
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