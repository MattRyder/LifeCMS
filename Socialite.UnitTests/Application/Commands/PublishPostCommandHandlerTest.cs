using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Commands.Posts;
using Xunit;

namespace Socialite.UnitTests.Application.Commands
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

            var result = await handler.Handle(publishCmd, default(CancellationToken));

            Assert.True(result);
        }

        [Fact]
        public async void Handle_ReturnsFalse_GivenNonExistentId()
        {
            var publishCmd = new PublishPostCommand(1);

            _postRepositoryMock.Setup(x => x.FindAsync(It.IsAny<int>())).Returns(new ValueTask<Post>((Post)null));

            var handler = new PublishPostCommandHandler(_postRepositoryMock.Object);

            var result = await handler.Handle(publishCmd, default(CancellationToken));

            Assert.False(result);
        }
    }
}