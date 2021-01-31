using LifeCMS.Services.ContentCreation.Domain.Interfaces;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Album Add(Album album);

        void Delete(Album album);
    }
}
