using System.Collections.Generic;
using System.Data;
using System.Linq;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Posts;
using Xunit;

namespace LifeCMS.Services.ContentCreation.IntegrationTests.Application.Queries
{
    public class PostQueriesTest : QueryTestBase
    {
        [Fact]
        public void FindAllAsync_ReturnsOk()
        {
            RunWithDbContext(_dbContext, async (context) =>
            {
                var postList = PostFactory.CreateList();

                context.PostStates.Attach(postList.First().State);
                
                context.Posts.Add(postList.First());

                await context.SaveChangesAsync();

                var postViewModels = context.Set<Post>().ToList().ConvertAll(PostViewModel.FromModel);

                var postQueries = new PostQueries(_dbConnectionFactory);

                var result = await postQueries.FindAllAsync() as List<PostViewModel>;

                Assert.NotNull(result);

                Assert.Equal(postViewModels.Count, result.Count);

                Assert.Equal(postViewModels, result);
            });
        }

        [Fact]
        public void FindPost_ReturnsOk()
        {
            RunWithDbContext(_dbContext, async (context) =>
            {
                var postList = PostFactory.CreateList();

                context.AttachRange(postList.Select(p => p.State));

                await context.Set<Post>().AddRangeAsync(postList);

                await context.SaveChangesAsync();

                var postViewModel = PostViewModel.FromModel(context.Set<Post>().First());

                var postQueries = new PostQueries(_dbConnectionFactory);

                var result = await postQueries.FindAsync(postViewModel.Id);

                Assert.NotNull(result);

                Assert.Equal(postViewModel, result);
            });
        }
    }
}