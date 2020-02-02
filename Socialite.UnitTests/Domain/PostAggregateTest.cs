using System;
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

            Assert.Equal(1, post.Events.Count);
        }

        [Fact]
        public void Constructor_ThrowsException_GivenInvalidTextParam()
        {
            Assert.Throws<PostDomainException>(() => new Post(new Guid(), "Title", null));
        }

        [Fact]
        public void Constructor_ThrowsException_GivenInvalidTitleParam()
        {
            Assert.Throws<PostDomainException>(() => new Post(new Guid(), null, "Text"));
        }


        [Fact]
        public void SetPublishedStatus_CreatesEvent_GivenValidDraft()
        {
            var post = PostFactory.Create();

            Assert.NotNull(post);

            post.SetPublishedState();

            Assert.Equal(2, post.Events.Count);
        }

        [Fact]
        public void SetPublishedStatus_ThrowsException_GivenPublishedPost()
        {
            var post = PostFactory.Create();

            Assert.NotNull(post);

            post.SetPublishedState();

            Assert.Equal(post.State, PostState.Published);

            Assert.Throws<PostDomainException>(() => post.SetPublishedState());
        }
    }
}