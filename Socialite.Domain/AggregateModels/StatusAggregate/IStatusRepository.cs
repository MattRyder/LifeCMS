using System.Collections.Generic;
using System.Threading.Tasks;
using Socialite.Domain.Interfaces;

namespace Socialite.Domain.AggregateModels.StatusAggregate
{
    public interface IStatusRepository : IRepository<Status>
    {
        List<Status> FindAll();

        Task<Status> FindAsync(int statusId);

        Status Add(Status status);

        void Delete(Status status);
    }
}