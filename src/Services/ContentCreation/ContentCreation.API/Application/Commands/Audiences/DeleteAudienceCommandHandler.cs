using System;
using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Services.Audiences;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Audiences
{
    public class DeleteAudienceCommandHandler : IRequestHandler<DeleteAudienceCommand, BasicResponse>
    {
        private readonly IAudienceRepository _audienceRepository;

        private readonly IAudienceLookupService _audienceLookupService;

        public DeleteAudienceCommandHandler(
            IAudienceRepository audienceRepository,
            IAudienceLookupService audienceLookupService)
        {
            _audienceRepository = audienceRepository;

            _audienceLookupService = audienceLookupService;
        }

        public async Task<BasicResponse> Handle(
            DeleteAudienceCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var audience = await _audienceLookupService.FindAudienceAsync(
                    request.AudienceId);

                _audienceRepository.Delete(audience);

                await _audienceRepository.UnitOfWork.SaveEntitiesAsync();

                return new BasicResponse()
                {
                    Success = true
                };
            }
            catch (Exception ex) when (
                ex is AudienceLookupServiceException ||
                ex is AudienceDomainException
            )
            {
                return new BasicResponse()
                {
                    Success = false,
                    Errors = new[] { ex.Message }
                };
            }
        }
    }
}
