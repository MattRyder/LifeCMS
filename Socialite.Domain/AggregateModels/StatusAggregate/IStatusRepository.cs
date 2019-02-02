using System.Collections.Generic;
using System.Threading.Tasks;
using Socialite.Domain.Interfaces;

namespace Socialite.Domain.AggregateModels.StatusAggregate
{
    public interface IStatusRepository : IRepository<Status>
    {
        Status Add(Status status);

        void Update(Status status);

        Task<Status> FindAsync(int statusId);

        List<Status> FindAll();
    }
}