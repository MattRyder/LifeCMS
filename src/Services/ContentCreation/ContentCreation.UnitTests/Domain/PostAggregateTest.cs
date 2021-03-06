using System;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.Domain.Exceptions;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Domain
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
            Assert.Throws<PostDomainException>(() => new Post(Guid.NewGuid(), "Title", null));
        }

        [Fact]
        public void Constructor_ThrowsException_GivenInvalidTitleParam()
        {
            Assert.Throws<PostDomainException>(() => new Post(Guid.NewGuid(), null, "Text"));
        }


        [Fact]
        public void SetPublishedStatus_CreatesEvent_GivenValidDraft()
        {
            var post = PostFactory.Create();

            Assert.NotNull(post);

            post.SetPublishedState();

            Assert.Equal(1, post.Events.Count);
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
