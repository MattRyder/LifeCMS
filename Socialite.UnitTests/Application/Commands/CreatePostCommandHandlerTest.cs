using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Infrastructure.DTO;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Commands.Posts;
using Xunit;

namespace Socialite.UnitTests.Application.Commands
{
    public class CreatePostCommandHandlerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IPostRepository> _postRepositoryMock;

        public CreatePostCommandHandlerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _postRepositoryMock = new Mock<IPostRepository>();
        }

        [Fact]
        public async void Handle_ShouldReturnTrue_GivenValidCommand()
        {
            var post = PostFactory.Create();

            var createPostCmd = new CreatePostCommand(post.Text);

            _postRepositoryMock.Setup(p => p.Add(post)).Returns(post);

            _postRepositoryMock.Setup(p => p.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var handler = new CreatePostCommandHandler(_postRepositoryMock.Object);

            var result = await handler.Handle(createPostCmd, default(CancellationToken));

            Assert.True(result);
        }
    }
}