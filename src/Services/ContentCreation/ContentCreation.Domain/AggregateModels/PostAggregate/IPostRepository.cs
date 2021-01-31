using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.Interfaces;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> FindAll();

        Post Add(Post post);

        void Delete(Post post);
    }
}
