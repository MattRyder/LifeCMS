using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using MySql.Data.MySqlClient;
using Respawn;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Infrastructure.Data;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Queries.Statuses;
using Socialite.WebAPI.Queries.Statuses;
using Xunit;

namespace Socialite.IntegrationTests.Application.Queries
{
    public class StatusQueriesTest
    {
        private readonly Checkpoint _checkpoint;
        private readonly SocialiteDbContext dbContext;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public StatusQueriesTest()
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

            dbContext = new SocialiteDbContext(contextOptions, null);

            dbContext.Database.EnsureDeleted();

            dbContext.Database.Migrate();

            // Required to store EMOJI
            // dbContext.Database.ExecuteSqlCommand($@"ALTER DATABASE CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");
        }

        [Fact]
        public void FindAllAsync_ReturnsOk()
        {
            // Required to store EMOJI
            // dbContext.Database.ExecuteSqlCommand($@"ALTER DATABASE CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");

            RunWithDbContext(dbContext, async (context) =>
            {
                var statusList = StatusFactory.CreateList();

                await context.Set<Status>().AddRangeAsync(statusList);

                await context.SaveChangesAsync();

                var statusViewModelList = context.Set<Status>().ToList().ConvertAll<StatusViewModel>(s => StatusViewModel.FromModel(s));

                var statusQueries = new StatusQueries(_dbConnectionFactory);

                var result = await statusQueries.FindAllAsync() as List<StatusViewModel>;

                Assert.NotNull(result);

                Assert.Equal(statusViewModelList.Count, result.Count);

                Assert.Equal(statusViewModelList, result);
            });
        }

        [Fact]
        public void FindStatus_ReturnsOk()
        {
            // Required to store EMOJI
            // dbContext.Database.ExecuteSqlCommand($@"ALTER DATABASE CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");

            RunWithDbContext(dbContext, async (context) =>
            {
                var statusList = StatusFactory.CreateList();

                await context.Set<Status>().AddRangeAsync(statusList);

                await context.SaveChangesAsync();

                var statusViewModel = StatusViewModel.FromModel(context.Set<Status>().First());

                var statusQueries = new StatusQueries(_dbConnectionFactory);

                var result = await statusQueries.FindStatus(statusViewModel.Id);

                Assert.NotNull(result);

                Assert.Equal(statusViewModel, result);
            });
        }


        private void RunWithDbContext(SocialiteDbContext dbContext, Action<SocialiteDbContext> assertFunc)
        {
            _checkpoint.Reset(_dbConnectionFactory.CreateConnection().ConnectionString);

            assertFunc(dbContext);
        }
    }
}