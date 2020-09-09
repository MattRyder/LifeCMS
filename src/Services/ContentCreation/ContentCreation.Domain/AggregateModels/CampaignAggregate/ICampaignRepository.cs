using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.Interfaces;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        ValueTask<Campaign> FindAsync(Guid campaignId);

        Campaign Add(Campaign campaign);

        void Update(Campaign campaign);

        void Delete(Campaign campaign);
    }
}