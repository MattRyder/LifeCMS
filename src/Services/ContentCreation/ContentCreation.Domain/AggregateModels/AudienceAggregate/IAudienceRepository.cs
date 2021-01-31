using LifeCMS.Services.ContentCreation.Domain.Interfaces;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate
{
    public interface IAudienceRepository : IRepository<Audience>
    {
        Subscriber FindSubscriberBySubscriberToken(string subscriberToken);

        void AddAsync(Audience audience);

        void Delete(Audience audience);
    }
}
