using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.Domain.Exceptions;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Domain
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