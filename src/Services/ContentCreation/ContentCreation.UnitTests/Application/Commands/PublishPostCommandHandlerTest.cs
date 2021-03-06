using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Posts;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Application.Commands
{
    public class PublishPostCommandHandlerTest
    {
        private readonly Mock<IPostRepository> _postRepositoryMock;

        public PublishPostCommandHandlerTest()
        {
            _postRepositoryMock = new Mock<IPostRepository>();
        }

        [Fact]
        public async void Handle_ReturnsTrue_GivenValidPostId()
        {
            var post = PostFactory.Create();

            var publishCmd = new PublishPostCommand(post.Id);

            _postRepositoryMock.Setup(x => x.FindAsync(post.Id)).ReturnsAsync(post);

            _postRepositoryMock.Setup(x => x.UnitOfWork.SaveEntitiesAsync()).ReturnsAsync(true);

            var handler = new PublishPostCommandHandler(_postRepositoryMock.Object);

            var result = await handler.Handle(publishCmd, default);

            Assert.True(result);
        }

        [Fact]
        public async void Handle_ReturnsFalse_GivenNonExistentId()
        {
            var publishCmd = new PublishPostCommand(Guid.NewGuid());

            _postRepositoryMock.Setup(x => x.FindAsync(It.IsAny<Guid>())).Returns(new ValueTask<Post>((Post)null));

            var handler = new PublishPostCommandHandler(_postRepositoryMock.Object);

            var result = await handler.Handle(publishCmd, default);

            Assert.False(result);
        }
    }
}
