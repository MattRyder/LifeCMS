using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Respawn;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.StatusAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Statuses;
using Xunit;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;

namespace LifeCMS.Services.ContentCreation.IntegrationTests.Application.Queries
{
    public class StatusQueriesTest
    {
        private readonly Checkpoint _checkpoint;
        private readonly ContentCreationDbContext _dbContext;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public StatusQueriesTest()
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

            // Required to store EMOJI
            // _dbContext.Database.ExecuteSqlCommand($@"ALTER DATABASE CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");
        }

        [Fact]
        public void FindAllAsync_ReturnsOk()
        {
            // Required to store EMOJI
            // _dbContext.Database.ExecuteSqlCommand($@"ALTER DATABASE CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");

            RunWithDbContext(_dbContext, async (context) =>
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
            // _dbContext.Database.ExecuteSqlCommand($@"ALTER DATABASE CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");

            RunWithDbContext(_dbContext, async (context) =>
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


        private void RunWithDbContext(ContentCreationDbContext dbContext, Action<ContentCreationDbContext> assertFunc)
        {
            _checkpoint.Reset(_dbConnectionFactory.CreateConnection().ConnectionString);

            assertFunc(dbContext);
        }
    }
}