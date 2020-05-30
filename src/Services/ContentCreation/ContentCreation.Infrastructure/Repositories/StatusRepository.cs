using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.StatusAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        public ContentCreationDbContext DbContext { get; private set; }

        public IUnitOfWork UnitOfWork => DbContext;

        public StatusRepository(ContentCreationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<Status> FindAll()
        {
            return DbContext.Set<Status>().ToList();
        }

        public async Task<Status> FindAsync(Guid statusId)
        {
            return await DbContext.Set<Status>().FindAsync(statusId);
        }

        public Status Add(Status status)
        {
            return DbContext.Set<Status>().Add(status).Entity;
        }

        public void Delete(Status status)
        {
            DbContext.Set<Status>().Remove(status);
        }
    }
}
