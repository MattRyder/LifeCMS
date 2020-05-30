using System;
using System.Threading.Tasks;

namespace LifeCMS.Services.ContentCreation.Domain.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveEntitiesAsync();
    }
}
