using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Repositories
{
    public class NewsletterRepository : INewsletterRepository
    {
        public ContentCreationDbContext DbContext { get; private set; }

        public IUnitOfWork UnitOfWork => DbContext;

        public NewsletterRepository(ContentCreationDbContext context)
        {
            DbContext = context;
        }

        public ValueTask<Newsletter> FindAsync(Guid newsletterId)
        {
            return DbContext.Set<Newsletter>().FindAsync(newsletterId);
        }

        public async void AddAsync(Newsletter newsletter)
        {
            await DbContext.AddAsync(newsletter);
        }

        public void Update(Newsletter newsletter)
        {
            DbContext.Set<Newsletter>().Update(newsletter);
        }

        public void Delete(Newsletter newsletter)
        {
            DbContext.Set<Newsletter>().Remove(newsletter);
        }
    }
}
