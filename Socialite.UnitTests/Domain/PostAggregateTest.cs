using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.Exceptions;
using Socialite.UnitTests.Factories;
using Xunit;

namespace Socialite.UnitTests.Domain
{
    public class PostAggregateTest
    {
        [Fact]
        public void Constructor_ReturnsPost_GivenValidParams()
        {
            var post = PostFactory.Create();

            Assert.NotNull(post);
        }

        [Fact]
        public void Constructor_ThrowsException_GivenInvalidTextParam()
        {
            Assert.Throws<PostDomainException>(() => new Post(null));
        }

        [Fact]
        public void NewDraft_ReturnsPost()
        {
            var post = Post.NewDraft();

            Assert.NotNull(post);

            Assert.Equal(PostState.Drafted, post.State);
        }


    }
}