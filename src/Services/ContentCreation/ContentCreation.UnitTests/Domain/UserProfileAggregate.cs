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
            var userProfile = UserProfileFactory.Create();

            Assert.NotNull(userProfile);
        }

        [Fact]
        public void Constructor_ThrowsException_GivenNullName()
        {
            var userProfile = UserProfileFactory.Create();

            Assert.Throws<UserProfileDomainException>(() => new UserProfile(userProfile.UserId, null, userProfile.EmailAddress));
        }

        [Fact]
        public void Constructor_ThrowsException_GivenNullEmail()
        {
            var userProfile = UserProfileFactory.Create();

            Assert.Throws<UserProfileDomainException>(() => new UserProfile(userProfile.UserId, userProfile.Name, null));
        }
    }
}