using System;
using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Services.Campaigns;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Campaigns
{
    public class UpdateCampaignNameCommandHandler : IRequestHandler<UpdateCampaignNameCommand, bool>
    {
        private readonly ICampaignRepository _campaignRepository;

        private readonly ILogger<UpdateCampaignNameCommandHandler> _logger;

        private readonly ICampaignLookupService _campaignLookupService;

        public UpdateCampaignNameCommandHandler(
            ICampaignRepository campaignRepository,
            ILogger<UpdateCampaignNameCommandHandler> logger,
            ICampaignLookupService campaignLookupService
        )
        {
            _campaignRepository = campaignRepository;

            _logger = logger;

            _campaignLookupService = campaignLookupService;
        }

        public async Task<bool> Handle(
            UpdateCampaignNameCommand request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var campaign = await _campaignLookupService.FindCampaign(request.CampaignId);

                campaign.UpdateName(request.Name);

                _campaignRepository.Update(campaign);

                return await _campaignRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex) when (
                ex is CampaignDomainException ||
                ex is CampaignLookupServiceException
            )
            {
                _logger.LogError(ex, null);

                return false;
            }
        }
    }
}