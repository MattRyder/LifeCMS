using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Repositories
{
    public class CampaignRepository : ICampaignRepository
    {
        public ContentCreationDbContext DbContext { get; private set; }

        public IUnitOfWork UnitOfWork => DbContext;

        public CampaignRepository(ContentCreationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ValueTask<Campaign> FindAsync(Guid id)
        {
            return DbContext.Set<Campaign>().FindAsync(id);
        }

        public Campaign Add(Campaign campaign)
        {
            return DbContext.Set<Campaign>().Add(campaign).Entity;
        }

        public void Update(Campaign campaign)
        {
            DbContext.Set<Campaign>().Update(campaign);
        }

        public void Delete(Campaign campaign)
        {
            DbContext.Set<Campaign>().Remove(campaign);
        }
    }
}
