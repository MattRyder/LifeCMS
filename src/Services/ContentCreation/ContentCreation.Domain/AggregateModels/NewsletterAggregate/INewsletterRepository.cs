using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.Interfaces;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate
{
    public interface INewsletterRepository : IRepository<Newsletter>
    {
        ValueTask<Newsletter> FindAsync(Guid newsletterId);

        void AddAsync(Newsletter newsletter);

        void Delete(Newsletter newsletter);
    }
}