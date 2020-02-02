using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Socialite.WebAPI.Queries.Posts
{
    public interface IPostQueries
    {
        Task<IEnumerable<PostViewModel>> FindAllAsync();

        Task<PostViewModel> FindAsync(Guid id);
    }
}