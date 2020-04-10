using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.Common;
using Socialite.Infrastructure.Data;

namespace Socialite.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        public SocialiteDbContext DbContext { get; private set; }

        public IUnitOfWork UnitOfWork => DbContext;

        public PostRepository(SocialiteDbContext context)
        {
            DbContext = context;
        }

        public IEnumerable<Post> FindAll()
        {
            return DbContext.Set<Post>().ToList();
        }

        public ValueTask<Post> FindAsync(Guid postId)
        {
            return DbContext.Set<Post>().FindAsync(postId);
        }

        public Post Add(Post post)
        {
            DbContext.Entry(post.State).State = EntityState.Unchanged;

            return DbContext.Add(post).Entity;
        }

        public void Delete(Post post)
        {
            DbContext.Set<Post>().Remove(post);
        }
    }
}