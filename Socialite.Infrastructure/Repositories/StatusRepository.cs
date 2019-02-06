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
        public StatusContext StatusContext { get; private set; }

        public IUnitOfWork UnitOfWork => StatusContext;

        public StatusRepository(StatusContext statusContext)
        {
            StatusContext = statusContext;
        }

        public List<Status> FindAll()
        {
            return StatusContext.Set<Status>().ToList();
        }

        public async Task<Status> FindAsync(int statusId)
        {
            return await StatusContext.Set<Status>().FindAsync(statusId);
        }

        public Status Add(Status status)
        {
            return StatusContext.Statuses.Add(status).Entity;
        }

        public void Delete(Status status)
        {
            StatusContext.Set<Status>().Remove(status);
        }
    }
}