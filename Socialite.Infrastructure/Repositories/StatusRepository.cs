using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Domain.Common;
using Socialite.Infrastructure.Data;

namespace Socialite.Infrastructure.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        public SocialiteDbContext DbContext { get; private set; }

        public IUnitOfWork UnitOfWork => DbContext;

        public StatusRepository(SocialiteDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<Status> FindAll()
        {
            return DbContext.Set<Status>().ToList();
        }

        public async Task<Status> FindAsync(int statusId)
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