using System.Threading.Tasks;
using LifeCMS.EventBus.Common.Interfaces;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Posts;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Application.Commands
{
    public class CreatePostCommandHandlerTest
    {
        private readonly Mock<IPostRepository> _postRepositoryMock;

        private readonly Mock<ILogger<CreatePostCommandHandler>> _loggerMock;

        private readonly Mock<IUserAccessor> _userAccessorMock;

        private readonly Mock<IEventBus> _eventBusMock;

        public CreatePostCommandHandlerTest()
        {
            _postRepositoryMock = new Mock<IPostRepository>();

            _loggerMock = new Mock<ILogger<CreatePostCommandHandler>>();

            _userAccessorMock = new Mock<IUserAccessor>();

            _eventBusMock = new Mock<IEventBus>();
        }

        [Fact]
        public async void Handle_ShouldReturnTrue_GivenValidCommand()
        {
            var post = PostFactory.Create();

            var createPostCmd = new CreatePostCommand(post.AuthorId, post.Title, post.Text);

            _postRepositoryMock.Setup(p => p.Add(post)).Returns(post);

            _postRepositoryMock.Setup(p => p.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var handler = new CreatePostCommandHandler(
                _postRepositoryMock.Object,
                _loggerMock.Object,
                _userAccessorMock.Object,
                _eventBusMock.Object
            );

            var result = await handler.Handle(createPostCmd, default);

            Assert.True(result);
        }
    }
}