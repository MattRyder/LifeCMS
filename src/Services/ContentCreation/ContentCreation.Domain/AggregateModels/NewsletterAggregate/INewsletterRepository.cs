using LifeCMS.Services.ContentCreation.Domain.Interfaces;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate
{
    public interface INewsletterRepository : IRepository<Newsletter>
    {
        void AddAsync(Newsletter newsletter);

        void Update(Newsletter newsletter);

        void Delete(Newsletter newsletter);
    }
}
