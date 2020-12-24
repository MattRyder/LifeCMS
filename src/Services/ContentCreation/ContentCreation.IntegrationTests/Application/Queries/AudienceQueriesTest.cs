using System;
using System.Linq;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Audiences;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using Xunit;

namespace LifeCMS.Services.ContentCreation.IntegrationTests.Application.Queries
{
    public class AudienceQueriesTest : QueryTestBase
    {
        [Fact]
        public void FindAudiences_ReturnsAudienceViewModel()
        {
            RunWithDbContext(_dbContext, async (dbContext) =>
            {
                var userId = Guid.NewGuid();

                var audience = AudienceFactory.Create(userId);

                await dbContext.Set<Audience>().AddAsync(audience);

                await dbContext.SaveChangesAsync();

                var audienceViewModel = dbContext
                    .Set<Audience>()
                    .ToList()
                    .ConvertAll(AudienceViewModel.FromModel);

                var queries = new AudienceQueries(_dbConnectionFactory);

                var result = await queries.FindAudiences(userId);

                Assert.Equal(
                    audienceViewModel.First(),
                    result.First()
                );
            });
        }
    }
}
