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

        public CreateCampaignCommandHandler(
            ICampaignRepository campaignRepository,
            ILogger<CreateCampaignCommandHandler> logger,
            IUserAccessor userAccessor,
            ICampaignValidationService campaignValidationService
        )
        {
            _campaignRepository = campaignRepository;

            _logger = logger;

            _userAccessor = userAccessor;

            _campaignValidationService = campaignValidationService;
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

                var campaign = new Campaign(
                    _userAccessor.Id,
                    request.NewsletterTemplateId,
                    request.UserProfileId,
                    request.Name,
                    request.Subject,
                    request.ScheduledDate,
                    request.UseSubscriberTimeZone
                );

                _campaignRepository.Add(campaign);

                return await _campaignRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex) when (
                ex is CampaignDomainException ||
                ex is CampaignValidationServiceException
            )
            {
                _logger.LogError(ex, null);

                return false;
            }
        }
    }
}