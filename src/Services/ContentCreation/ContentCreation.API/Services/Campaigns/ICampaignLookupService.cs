using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;

namespace LifeCMS.Services.ContentCreation.API.Services.Campaigns
{
    public interface ICampaignLookupService
    {
        Task<Campaign> FindCampaign(Guid campaignId);
    }
}