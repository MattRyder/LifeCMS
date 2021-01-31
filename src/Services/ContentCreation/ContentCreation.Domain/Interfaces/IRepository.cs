using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.Interfaces
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        ValueTask<T> FindAsync(Guid id);

        IUnitOfWork UnitOfWork { get; }
    }
}
