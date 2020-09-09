using System;
using System.Security.Claims;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Authorization;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using Microsoft.AspNetCore.Authorization;

namespace LifeCMS.Services.ContentCreation.API.Services.Campaigns
{
    public class CampaignValidationServiceException : Exception
    {
        public CampaignValidationServiceException(string message) : base(message) { }
    }

    public class CampaignValidationService : ICampaignValidationService
    {
        private readonly IAuthorizationService _authorizationService;

        private readonly INewsletterRepository _newsletterRepository;

        private readonly IUserProfileRepository _userProfileRepository;

        public CampaignValidationService(
            IAuthorizationService authorizationService,
            INewsletterRepository newsletterRepository,
            IUserProfileRepository userProfileRepository
        )
        {
            _authorizationService = authorizationService;

            _newsletterRepository = newsletterRepository;

            _userProfileRepository = userProfileRepository;
        }

        public async Task<bool> ValidateDependencyOwnership(
            ClaimsPrincipal user,
            Guid newsletterTemplateId,
            Guid userProfileId)
        {
            return await OwnsNewsletter(user, newsletterTemplateId) &&
                    await OwnsUserProfile(user, userProfileId);
        }

        private async Task<bool> OwnsNewsletter(ClaimsPrincipal user, Guid newsletterId)
        {
            var newsletter = await _newsletterRepository.FindAsync(newsletterId);

            var ownsResource = await _authorizationService.OwnsResource(
                user,
                newsletter
            );

            return ownsResource
                ? ownsResource
                : throw new CampaignValidationServiceException(
                    "Failed to validate that the User owns the Newsletter Template."
                );
        }

        private async Task<bool> OwnsUserProfile(ClaimsPrincipal user, Guid userProfileId)
        {
            var userProfile = await _userProfileRepository.FindAsync(userProfileId);

            var ownsResource = await _authorizationService.OwnsResource(
                user,
                userProfile
            );

            return ownsResource
                  ? ownsResource
                  : throw new CampaignValidationServiceException(
                      "Failed to validate that the User owns the Newsletter Template."
                  );
        }
    }
}