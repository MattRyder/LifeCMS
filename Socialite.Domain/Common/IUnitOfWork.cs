using System;
using System.Threading.Tasks;

namespace Socialite.Domain.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveEntitiesAsync();
    }
}
