using System.Linq;
using Xunit;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.API.Application.Queries.UserProfiles;

namespace LifeCMS.Services.ContentCreation.IntegrationTests.Application.Queries
{
    public class UserProfileQueriesTest : QueryTestBase
    {
        [Fact]
        public void FindUserProfile_ReturnsOk()
        {
            RunWithDbContext(_dbContext, async (context) =>
            {
                var userProfile = UserProfileFactory.Create();

                await context.Set<UserProfile>().AddRangeAsync(userProfile);

                await context.SaveChangesAsync();

                var vm = UserProfileViewModel.FromModel(context.Set<UserProfile>().First());

                var queries = new UserProfileQueries(_dbConnectionFactory);

                var result = await queries.FindUserProfile(vm.Id);

                Assert.NotNull(result);

                Assert.Equal(vm, result);
            });
        }
    }
}