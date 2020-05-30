using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.Interfaces;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> FindAll();

        ValueTask<Post> FindAsync(Guid postId);

        Post Add(Post post);

        void Delete(Post post);
    }
}