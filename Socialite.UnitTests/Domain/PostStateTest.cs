using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.Exceptions;
using Xunit;

namespace Socialite.UnitTests.Domain
{
    public class PostStateTest
    {
        [Fact]
        public void FromName_ReturnsPostState_GivenValidName()
        {
            var stateName = "drafted";

            var state = PostState.FromName(stateName);

            Assert.NotNull(state);

            Assert.Equal(stateName, state.Name);
        }

        [Fact]
        public void FromName_ThrowsPostStateDomainException_GivenInvalidName()
        {
            Assert.Throws<PostDomainException>(() => PostState.FromName(string.Empty));
        }



    }
}