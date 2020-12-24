using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Audiences
{
    public class CreateAudienceCommandHandler
        : IRequestHandler<CreateAudienceCommand, BasicResponse>
    {
        private readonly IAudienceRepository _audienceRepository;

        private readonly ILogger<CreateAudienceCommandHandler> _logger;

        private readonly IUserAccessor _userAccessor;

        public CreateAudienceCommandHandler(
            IAudienceRepository audienceRepository,
            ILogger<CreateAudienceCommandHandler> logger,
            IUserAccessor userAccessor
        )
        {
            _audienceRepository = audienceRepository;

            _logger = logger;

            _userAccessor = userAccessor;
        }

        public async Task<BasicResponse> Handle(
            CreateAudienceCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var audience = new Audience(_userAccessor.Id, request.Name);

                _audienceRepository.AddAsync(audience);

                await _audienceRepository.UnitOfWork.SaveEntitiesAsync();

                return new BasicResponse()
                {
                    Success = true,
                    Data = new { audience.Id },
                };
            }
            catch (AudienceDomainException ex)
            {
                _logger.LogError(ex, ex.Message);

                return new BasicResponse()
                {
                    Success = false,
                    Errors = new[] { ex.Message }
                };
            }
        }
    }
}
