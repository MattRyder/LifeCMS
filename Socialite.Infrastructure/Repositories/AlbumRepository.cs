using System.Threading.Tasks;
using Socialite.Domain.AggregateModels.AlbumAggregate;
using Socialite.Domain.Common;
using Socialite.Infrastructure.Data;

namespace Socialite.Infrastructure.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        public SocialiteDbContext DbContext { get; private set; }

        public IUnitOfWork UnitOfWork => DbContext;

        public AlbumRepository(SocialiteDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ValueTask<Album> FindAsync(int id)
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