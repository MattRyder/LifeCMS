using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        public ContentCreationDbContext DbContext { get; private set; }

        public IUnitOfWork UnitOfWork => DbContext;

        public PostRepository(ContentCreationDbContext context)
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
