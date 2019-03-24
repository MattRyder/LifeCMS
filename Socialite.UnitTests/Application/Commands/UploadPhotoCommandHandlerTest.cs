using System.Threading;
using System.Threading.Tasks;
using Bogus;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using Socialite.Domain.AggregateModels.AlbumAggregate;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Commands.Albums;
using Socialite.WebAPI.Application.Commands.Posts;
using Socialite.WebAPI.Infrastructure.Services;
using Xunit;

namespace Socialite.UnitTests.Application.Commands
{
    public class UploadPhotoCommandHandlerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IAlbumRepository> _albumRepositoryMock;
        private readonly Mock<IImageUploadService> _imageUploadServiceMock;

        public UploadPhotoCommandHandlerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _albumRepositoryMock = new Mock<IAlbumRepository>();
            _imageUploadServiceMock = new Mock<IImageUploadService>();
        }

        [Fact]
        public async void Handle_ShouldReturnTrue_GivenValidCommand()
        {
            var f = new Faker();

            var album = AlbumFactory.Create();

            var uploadPhotoCmd = new UploadPhotoCommand(album.Id, f.Lorem.Word(), null, f.Lorem.Sentence());

            _imageUploadServiceMock.Setup(s => s.UploadAsync(It.IsAny<IFormFile>())).ReturnsAsync(new System.Uri(new Faker().Internet.UrlWithPath()));

            _albumRepositoryMock.Setup(p => p.FindAsync(It.IsAny<int>())).ReturnsAsync(album);

            _albumRepositoryMock.Setup(p => p.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var handler = new UploadPhotoCommandHandler(_albumRepositoryMock.Object, _imageUploadServiceMock.Object);

            var result = await handler.Handle(uploadPhotoCmd, default(CancellationToken));

            Assert.True(result);
        }

        [Fact]
        public async void Handle_ShouldReturnFalse_GivenInvalidCommand()
        {
            var f = new Faker();

            var album = AlbumFactory.Create();

            var uploadPhotoCmd = new UploadPhotoCommand(0, null, null, null);

            _imageUploadServiceMock.Setup(s => s.UploadAsync(It.IsAny<IFormFile>())).ReturnsAsync(new System.Uri(new Faker().Internet.UrlWithPath()));

            _albumRepositoryMock.Setup(p => p.FindAsync(It.IsAny<int>())).ReturnsAsync(album);

            _albumRepositoryMock.Setup(p => p.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var handler = new UploadPhotoCommandHandler(_albumRepositoryMock.Object, _imageUploadServiceMock.Object);

            var result = await handler.Handle(uploadPhotoCmd, default(CancellationToken));

            Assert.False(result);
        }

        [Fact]
        public async void Handle_ShouldReturnFalse_WhenFindAsyncReturnsNull()
        {
            var f = new Faker();

            var uploadPhotoCmd = new UploadPhotoCommand(0, f.Lorem.Word(), null, f.Lorem.Sentence());

            _imageUploadServiceMock.Setup(s => s.UploadAsync(It.IsAny<IFormFile>())).ReturnsAsync(new System.Uri(new Faker().Internet.UrlWithPath()));

            _albumRepositoryMock.Setup(p => p.FindAsync(It.IsAny<int>())).ReturnsAsync(null as Album);

            _albumRepositoryMock.Setup(p => p.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var handler = new UploadPhotoCommandHandler(_albumRepositoryMock.Object, _imageUploadServiceMock.Object);

            var result = await handler.Handle(uploadPhotoCmd, default(CancellationToken));

            Assert.False(result);
        }
    }
}