using System;
using System.Linq;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Campaigns;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using LifeCMS.Services.ContentCreation.IntegrationTests.Comparers;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using Xunit;

namespace LifeCMS.Services.ContentCreation.IntegrationTests.Application.Queries
{
    public class CampaignQueriesTest : QueryTestBase
    {
        [Fact]
        public void FindCampaigns_ReturnsCampaignViewModel()
        {
            RunWithDbContext(_dbContext, async (dbContext) =>
            {
                var userId = Guid.NewGuid();

                var campaign = CampaignFactory.Create(
                    userId,
                    Guid.NewGuid(),
                    Guid.NewGuid()
                );

                await dbContext.Set<Campaign>().AddAsync(campaign);

                await dbContext.SaveChangesAsync();

                var campaignViewModel = dbContext
                    .Set<Campaign>()
                    .ToList()
                    .ConvertAll(CampaignViewModel.FromModel);

                var queries = new CampaignQueries(_dbConnectionFactory);

                var result = await queries.FindCampaigns(userId);

                Assert.Equal(
                    campaignViewModel.First(),
                    result.First(),
                    new CampaignViewModelComparer()
                );
            });
        }
    }
}