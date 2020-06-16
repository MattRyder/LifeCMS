using System.Collections.Generic;
using System.Linq;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.StatusAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Statuses;
using Xunit;

namespace LifeCMS.Services.ContentCreation.IntegrationTests.Application.Queries
{
    public class StatusQueriesTest : QueryTestBase
    {
        [Fact]
        public void FindAllAsync_ReturnsOk()
        {
            RunWithDbContext(_dbContext, async (context) =>
            {
                var statusList = StatusFactory.CreateList();

                await context.Set<Status>().AddRangeAsync(statusList);

                await context.SaveChangesAsync();

                var statusViewModelList = context.Set<Status>().ToList().ConvertAll(StatusViewModel.FromModel);

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
    }
}