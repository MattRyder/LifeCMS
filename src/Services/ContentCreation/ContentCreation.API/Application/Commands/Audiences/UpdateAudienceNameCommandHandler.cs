using System;
using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Services.Audiences;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Audiences
{
    public class UpdateAudienceNameCommandHandler
        : IRequestHandler<UpdateAudienceNameCommand, BasicResponse>
    {

        private readonly IAudienceRepository _audienceRepository;

        private readonly IAudienceLookupService _audienceLookupService;

        private readonly ILogger<UpdateAudienceNameCommandHandler> _logger;

        public UpdateAudienceNameCommandHandler(
            IAudienceRepository audienceRepository,
            IAudienceLookupService audienceLookupService,
            ILogger<UpdateAudienceNameCommandHandler> logger)
        {
            _audienceRepository = audienceRepository;

            _audienceLookupService = audienceLookupService;

            _logger = logger;
        }

        public async Task<BasicResponse> Handle(
            UpdateAudienceNameCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var audience = await _audienceLookupService.FindAudienceAsync(request.AudienceId);

                audience.UpdateName(request.Name);

                await _audienceRepository.UnitOfWork.SaveEntitiesAsync();

                return new BasicResponse()
                {
                    Success = true,
                };
            }
            catch (Exception ex) when (
              ex is AudienceLookupServiceException ||
              ex is AudienceDomainException
          )
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
