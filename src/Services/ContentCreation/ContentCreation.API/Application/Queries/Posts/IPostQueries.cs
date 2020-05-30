using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Posts
{
    public interface IPostQueries
    {
        Task<IEnumerable<PostViewModel>> FindAllAsync();

        Task<PostViewModel> FindAsync(Guid id);
    }
}