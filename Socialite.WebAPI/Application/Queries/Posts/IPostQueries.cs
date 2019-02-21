using System.Collections.Generic;
using System.Threading.Tasks;
using Socialite.Infrastructure.DTO;

namespace  Socialite.WebAPI.Queries.Posts
{
    public interface IPostQueries
    {
        Task<IEnumerable<PostViewModel>> FindAllAsync();

        Task<PostViewModel> FindAsync(int id);
    }
}