using Socialite.Domain.Common;
using System.Collections.Generic;

namespace Socialite.Domain.Interfaces
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}