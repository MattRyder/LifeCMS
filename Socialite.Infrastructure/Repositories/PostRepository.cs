using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.Common;
using Socialite.Infrastructure.Data;

namespace Socialite.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        public SocialiteDbContext DbContext { get; private set; }

        public IUnitOfWork UnitOfWork => DbContext;

        public IEnumerable<Post> FindAll()
        {
            return DbContext.Set<Post>().ToList();
        }

        public Task<Post> FindAsync(int postId)
        {
            return DbContext.Set<Post>().FindAsync(postId);
        }

        public Post Add(Post post)
        {
            return DbContext.Add(post).Entity;
        }

        public void Delete(Post post)
        {
            DbContext.Set<Post>().Remove(post);
        }
    }
}