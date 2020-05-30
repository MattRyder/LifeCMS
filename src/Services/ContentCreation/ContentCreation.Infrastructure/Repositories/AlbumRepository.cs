using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        public ContentCreationDbContext DbContext { get; private set; }

        public IUnitOfWork UnitOfWork => DbContext;

        public AlbumRepository(ContentCreationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ValueTask<Album> FindAsync(Guid id)
        {
            return DbContext.Set<Album>().FindAsync(id);
        }

        public Album Add(Album album)
        {
            return DbContext.Set<Album>().Add(album).Entity;
        }

        public void Delete(Album album)
        {
            DbContext.Set<Album>().Remove(album);
        }

    }
}
