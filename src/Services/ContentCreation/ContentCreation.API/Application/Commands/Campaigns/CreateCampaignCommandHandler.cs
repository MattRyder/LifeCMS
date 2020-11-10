using System;
using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Services.Campaigns;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Campaigns
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, bool>
    {
        private readonly ICampaignRepository _campaignRepository;

        private readonly ILogger<CreateCampaignCommandHandler> _logger;

        private readonly IUserAccessor _userAccessor;

        private readonly ICampaignValidationService _campaignValidationService;

        private readonly ICampaignDeliveryService _campaignDeliveryService;

        public CreateCampaignCommandHandler(
            ICampaignRepository campaignRepository,
            ILogger<CreateCampaignCommandHandler> logger,
            IUserAccessor userAccessor,
            ICampaignValidationService campaignValidationService,
            ICampaignDeliveryService campaignDeliveryService
        )
        {
            _campaignRepository = campaignRepository;

            _logger = logger;

            _userAccessor = userAccessor;

            _campaignValidationService = campaignValidationService;

            _campaignDeliveryService = campaignDeliveryService;
        }

        public async Task<bool> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _campaignValidationService.ValidateDependencyOwnership(
                    _userAccessor.User,
                    request.NewsletterTemplateId,
                    request.UserProfileId
                );

                var campaign = CreateCampaign(request);

                await SaveCampaign(campaign);

                RaiseCampaignDeliveryEvent(campaign.Id);

                return true;
            }
            catch (CampaignValidationServiceException ex)
            {
                _logger.LogError(ex, null);

                return false;
            }
        }

        private Campaign CreateCampaign(CreateCampaignCommand request)
        {
            return new Campaign(
                _userAccessor.Id,
                request.NewsletterTemplateId,
                request.UserProfileId,
                request.Name,
                request.Subject,
                request.ScheduledDate,
                request.UseSubscriberTimeZone
            );
        }

        private async Task<bool> SaveCampaign(Campaign campaign)
        {
            _campaignRepository.Add(campaign);

            return await _campaignRepository.UnitOfWork.SaveEntitiesAsync();
        }

        private void RaiseCampaignDeliveryEvent(Guid campaignId)
        {
            _campaignDeliveryService.Execute(campaignId);
        }
    }
}
