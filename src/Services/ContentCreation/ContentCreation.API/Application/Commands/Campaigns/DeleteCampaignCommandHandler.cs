using System;
using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Services.Campaigns;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Campaigns
{
    public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, bool>
    {
        private readonly ICampaignRepository _campaignRepository;

        private readonly ICampaignLookupService _campaignLookupService;

        private readonly ILogger<DeleteCampaignCommandHandler> _logger;

        public DeleteCampaignCommandHandler(
            ICampaignRepository campaignRepository,
            ICampaignLookupService campaignLookupService,
            ILogger<DeleteCampaignCommandHandler> logger
        )
        {
            _campaignRepository = campaignRepository;
            _campaignLookupService = campaignLookupService;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var campaign = await _campaignLookupService.FindCampaign(request.CampaignId);

                _campaignRepository.Delete(campaign);

                return await _campaignRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex) when (
                ex is CampaignDomainException ||
                ex is CampaignLookupServiceException
            )
            {
                _logger.LogError(ex, ex.Message);

                return false;
            }
        }
    }
}