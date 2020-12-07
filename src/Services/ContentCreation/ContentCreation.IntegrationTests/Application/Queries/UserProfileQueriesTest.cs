using System.Linq;
using Dapper;
using Xunit;
using LifeCMS.Services.ContentCreation.API.Application.Queries.UserProfiles;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Dapper;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using System;

namespace LifeCMS.Services.ContentCreation.IntegrationTests.Application.Queries
{
    public class UserProfileQueriesTest : QueryTestBase
    {
        [Fact]
        public void FindUserProfiles_ReturnsOk()
        {
            SqlMapper.AddTypeHandler(new UriTypeHandler());

            RunWithDbContext(_dbContext, async (context) =>
            {
                var userId = Guid.NewGuid();

                var userProfileList = UserProfileFactory.CreateList(userId);

                await context.Set<UserProfile>().AddRangeAsync(userProfileList);

                await context.SaveChangesAsync();

                var ids = userProfileList.Select(u => u.Id);

                var vm = context.Set<UserProfile>().Where(u => ids.Contains(u.Id)).Select(UserProfileViewModel.FromModel);

                var queries = new UserProfileQueries(_dbConnectionFactory);

                var result = await queries.FindUserProfiles(userId);

                Assert.NotNull(result);

                Assert.Equal(vm, result);
            });
        }
    }
}
