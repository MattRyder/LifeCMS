using LifeCMS.Services.ContentCreation.Domain.Interfaces;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        Campaign Add(Campaign campaign);

        void Update(Campaign campaign);

        void Delete(Campaign campaign);
    }
}
