using System;

namespace LifeCMS.Services.ContentCreation.API.Services.Campaigns
{
    public interface ICampaignDeliveryService
    {
        void Execute(Guid campaignId);
    }
}
