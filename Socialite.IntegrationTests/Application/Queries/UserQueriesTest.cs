using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Respawn;
using Socialite.Domain.AggregateModels.AlbumAggregate;
using Socialite.Domain.AggregateModels.UsersAggregate;
using Socialite.Infrastructure.Data;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Queries.Albums;
using Socialite.WebAPI.Application.Queries.Users;
using Xunit;

namespace Socialite.IntegrationTests.Application.Queries
{
    public class UserQueriesTest
    {
        private readonly Checkpoint _checkpoint;
        private readonly SocialiteDbContext dbContext;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UserQueriesTest()
        {
            var integrationConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__SocialiteIntegrationTests");

            _checkpoint = new Checkpoint
            {
                SchemasToInclude = new[]
                {
                    "public"
                },
                DbAdapter = DbAdapter.MySql,
            };

            _dbConnectionFactory = new MySqlDbConnectionFactory(integrationConnectionString);

            var contextOptions = new DbContextOptionsBuilder<SocialiteDbContext>().UseMySql(_dbConnectionFactory.CreateConnection().ConnectionString).Options;

            dbContext = new SocialiteDbContext(contextOptions, null, null);

            dbContext.Database.EnsureDeleted();

            dbContext.Database.EnsureCreated();
        }

        [Fact]
        public void FindAsync_ReturnsOk()
        {
            RunWithDbContext(dbContext, async (context) =>
            {
                var user = UserFactory.Create();

                context.Users.Add(user);

                await context.SaveChangesAsync();

                var userViewModel = UserViewModel.FromModel(user);

                var userQueries = new UserQueries(_dbConnectionFactory);

                var result = await userQueries.FindAsync(user.Id) as UserViewModel;

                Assert.NotNull(result);

                Assert.Equal(userViewModel, result);
            });
        }

        [Fact]
        public void FindAsync_ThrowsException_GivenInvalidId()
        {
            RunWithDbContext(dbContext, async (context) =>
            {
                var user = UserFactory.Create();

                context.Users.Add(user);

                await context.SaveChangesAsync();

                var userQueries = new UserQueries(_dbConnectionFactory);

                await Assert.ThrowsAsync<KeyNotFoundException>(() => userQueries.FindAsync(999));
            });
        }

        private void RunWithDbContext(SocialiteDbContext dbContext, Action<SocialiteDbContext> assertFunc)
        {
            _checkpoint.Reset(_dbConnectionFactory.CreateConnection().ConnectionString);

            assertFunc(dbContext);
        }
    }
}