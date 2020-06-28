using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Domain
{
    public class UserProfileAggregateTest
    {
        [Fact]
        public void Constructor_ReturnsUserProfile_WhenValid()
        {
            var userProfile = UserProfileFactory.Create(null);

            Assert.NotNull(userProfile);
        }
    }
}