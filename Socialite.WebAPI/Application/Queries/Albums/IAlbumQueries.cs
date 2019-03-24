using System.Collections.Generic;
using System.Threading.Tasks;

namespace Socialite.WebAPI.Application.Queries.Albums
{
    public interface IAlbumQueries
    {
        Task<IEnumerable<AlbumViewModel>> FindAllAsync();

        Task<AlbumViewModel> FindAsync(int id);
    }
}