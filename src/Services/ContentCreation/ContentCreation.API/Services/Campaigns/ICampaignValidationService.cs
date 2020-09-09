using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LifeCMS.Services.ContentCreation.API.Services.Campaigns
{
    public interface ICampaignValidationService
    {
        Task<bool> ValidateDependencyOwnership(
            ClaimsPrincipal user,
            Guid newsletterTemplateId,
            Guid userProfileId
        );
    }
}