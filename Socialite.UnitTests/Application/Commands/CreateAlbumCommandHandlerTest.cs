using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Socialite.Domain.AggregateModels.AlbumAggregate;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Commands.Albums;
using Socialite.WebAPI.Application.Commands.Posts;
using Xunit;

namespace Socialite.UnitTests.Application.Commands
{
    public class CreateAlbumCommandHandlerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IAlbumRepository> _albumRepositoryMock;

        public CreateAlbumCommandHandlerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _albumRepositoryMock = new Mock<IAlbumRepository>();
        }

        [Fact]
        public async void Handle_ShouldReturnTrue_GivenValidCommand()
        {
            var album = AlbumFactory.Create();

            var createPostCmd = new CreateAlbumCommand(album.Name);

            _albumRepositoryMock.Setup(p => p.Add(album)).Returns(album);

            _albumRepositoryMock.Setup(p => p.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var handler = new CreateAlbumCommandHandler(_albumRepositoryMock.Object);

            var result = await handler.Handle(createPostCmd, default(CancellationToken));

            Assert.True(result);
        }

        [Fact]
        public async void Handle_ShouldReturnFalse_GivenValidCommand()
        {
            var createPostCmd = new CreateAlbumCommand(string.Empty);

            _albumRepositoryMock.Setup(p => p.Add(It.IsAny<Album>()));

            var handler = new CreateAlbumCommandHandler(_albumRepositoryMock.Object);

            var result = await handler.Handle(createPostCmd, default(CancellationToken));

            Assert.False(result);
        }
    }
}