using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.API.Services.Lookup
{
    public interface ILookupService<T> where T : BaseEntity, IAggregateRoot
    {
        Task<T> FindEntityByIdAsync(Guid id);
    }
}
