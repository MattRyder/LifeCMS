using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.Interfaces;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate
{
    public interface IAlbumRepository : IRepository<Album>
    {
        ValueTask<Album> FindAsync(Guid id);

        Album Add(Album album);

        void Delete(Album album);
    }
}