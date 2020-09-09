using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Authorization;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LifeCMS.Services.ContentCreation.API.Services.Campaigns
{
    public class CampaignLookupServiceException : Exception
    {
        public CampaignLookupServiceException(string message) : base(message) { }
    }

    public class CampaignLookupService : ICampaignLookupService
    {
        private readonly ICampaignRepository _campaignRepository;

        private readonly IUserAccessor _userAccessor;

        private readonly IAuthorizationService _authorizationService;

        public CampaignLookupService(
            ICampaignRepository campaignRepository,
            IUserAccessor userAccessor,
            IAuthorizationService authorizationService
        )
        {
            _campaignRepository = campaignRepository;

            _userAccessor = userAccessor;

            _authorizationService = authorizationService;
        }

        public async Task<Campaign> FindCampaign(Guid campaignId)
        {
            var campaign = await _campaignRepository.FindAsync(campaignId);

            if (campaign is null)
            {
                throw new CampaignLookupServiceException(
                    "Failed to find the Campaign for the given Campaign Id"
                );
            }

            var ownsResource = await _authorizationService.OwnsResource(
                _userAccessor.User,
                campaign
            );

            return ownsResource
            ? campaign
            : throw new CampaignLookupServiceException(
                "Failed to validate ownership of the Campaign."
            );

        }
    }
}