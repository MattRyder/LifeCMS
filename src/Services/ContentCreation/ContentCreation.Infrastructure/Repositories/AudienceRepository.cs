using System;
using System.Linq;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Repositories
{
    public class AudienceRepository : IAudienceRepository
    {
        public ContentCreationDbContext DbContext { get; private set; }

        public IUnitOfWork UnitOfWork => DbContext;

        public AudienceRepository(ContentCreationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ValueTask<Audience> FindAsync(Guid id)
        {
            return DbContext.Set<Audience>().FindAsync(id);
        }

        public void AddAsync(Audience audience)
        {
            DbContext.Set<Audience>().AddAsync(audience);
        }

        public void Delete(Audience audience)
        {
            DbContext.Set<Audience>().Remove(audience);
        }

        public Subscriber FindSubscriberBySubscriberToken(string subscriberToken)
        {
            return DbContext
                .Subscribers
                .Single(x => x.SubscriberToken.Equals(subscriberToken));
        }
    }
}
