using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Respawn;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Posts;
using Xunit;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;

namespace LifeCMS.Services.ContentCreation.IntegrationTests.Application.Queries
{
    public class PostQueriesTest
    {
        private readonly Checkpoint _checkpoint;
        private readonly ContentCreationDbContext _dbContext;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public PostQueriesTest()
        {
            var integrationConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__LifeCMSIntegrationTests");

            _checkpoint = new Checkpoint
            {
                SchemasToInclude = new[]
                {
                    "public"
                },
                DbAdapter = DbAdapter.MySql,
            };

            _dbConnectionFactory = new MySqlDbConnectionFactory(integrationConnectionString);

            var contextOptions = new DbContextOptionsBuilder<ContentCreationDbContext>().UseMySql(_dbConnectionFactory.CreateConnection().ConnectionString).Options;

            _dbContext = new ContentCreationDbContext(contextOptions, null);

            _dbContext.Database.EnsureDeleted();

            _dbContext.Database.Migrate();
        }

        [Fact]
        public void FindAllAsync_ReturnsOk()
        {
            RunWithDbContext(_dbContext, async (context) =>
            {
                var postList = PostFactory.CreateList();

                context.PostStates.Attach(postList.First().State);
                
                context.Posts.Add(postList.First());

                await context.SaveChangesAsync();

                var postViewModels = context.Set<Post>().ToList().ConvertAll(p => PostViewModel.FromModel(p));

                var postQueries = new PostQueries(_dbConnectionFactory);

                var result = await postQueries.FindAllAsync() as List<PostViewModel>;

                Assert.NotNull(result);

                Assert.Equal(postViewModels.Count, result.Count);

                Assert.Equal(postViewModels, result);
            });
        }

        [Fact]
        public void FindStatus_ReturnsOk()
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


        private void RunWithDbContext(ContentCreationDbContext dbContext, Action<ContentCreationDbContext> assertFunc)
        {
            _checkpoint.Reset(_dbConnectionFactory.CreateConnection().ConnectionString);

            assertFunc(dbContext);
        }
    }
}