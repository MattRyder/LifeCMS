using System.Collections.Generic;
using System.Threading.Tasks;
using Socialite.Domain.Interfaces;

namespace Socialite.Domain.AggregateModels.PostAggregate
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> FindAll();

        ValueTask<Post> FindAsync(int postId);

        Post Add(Post post);

        void Delete(Post post);
    }
}