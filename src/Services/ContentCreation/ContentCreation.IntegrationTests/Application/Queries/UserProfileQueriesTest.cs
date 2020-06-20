using System.Linq;
using Dapper;
using Xunit;
using LifeCMS.Services.ContentCreation.API.Application.Queries.UserProfiles;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Dapper;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;

namespace LifeCMS.Services.ContentCreation.IntegrationTests.Application.Queries
{
    public class UserProfileQueriesTest : QueryTestBase
    {
        [Fact]
        public void FindUserProfile_ReturnsOk()
        {
            SqlMapper.AddTypeHandler(new UriTypeHandler());

            RunWithDbContext(_dbContext, async (context) =>
            {
                var userProfile = UserProfileFactory.Create();

                await context.Set<UserProfile>().AddAsync(userProfile);

                await context.SaveChangesAsync();

                var vm = UserProfileViewModel.FromModel(context.Set<UserProfile>().First());

                var queries = new UserProfileQueries(_dbConnectionFactory);

                var result = await queries.FindUserProfile(vm.UserId);

                Assert.NotNull(result);

                Assert.Equal(vm, result);
            });
        }
    }
}