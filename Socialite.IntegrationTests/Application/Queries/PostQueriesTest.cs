using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using MySql.Data.MySqlClient;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Infrastructure.Data;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Queries.Posts;
using Socialite.WebAPI.Queries.Posts;
using Xunit;

namespace Socialite.IntegrationTests.Application.Queries
{
    public class PostQueriesTest
    {
        private readonly SocialiteDbContext dbContext;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public PostQueriesTest()
        {
            var integrationConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__SocialiteIntegrationTests");

            _dbConnectionFactory = new MySqlDbConnectionFactory(integrationConnectionString);

            var contextOptions = new DbContextOptionsBuilder<SocialiteDbContext>().UseMySql(_dbConnectionFactory.CreateConnection().ConnectionString).Options;

            dbContext = new SocialiteDbContext(contextOptions, null, null);

            try
            {
                dbContext.Database.Migrate();
            }
            catch (MySqlException)
            {
                // Pomelo MySQL throws if it doesn't exist, instead of returning false, as per docs?
            }
        }

        [Fact]
        public void FindAllAsync_ReturnsOk()
        {
            RunWithDbContext(dbContext, async (context) =>
            {
                var postList = PostFactory.CreateList();

                context.PostStates.Attach(postList.First().State);
                context.Posts.Add(postList.First());

                await context.SaveChangesAsync();

                var postViewModels = context.Set<Post>().ToList().ConvertAll<PostViewModel>(p => PostViewModel.FromModel(p));

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
            RunWithDbContext(dbContext, async (context) =>
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


        private void RunWithDbContext(SocialiteDbContext dbContext, Action<SocialiteDbContext> assertFunc)
        {
            try
            {

                dbContext.Database.ExecuteSqlCommand("DELETE FROM Posts;");
            }
            catch (MySqlException)
            {

            }

            assertFunc(dbContext);
        }
    }
}