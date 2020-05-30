using System;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Albums;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Services;

namespace LifeCMS.Services.ContentCreation.UnitTests.Application.Commands
{
    public class UploadPhotoCommandHandlerTest
    {
        private readonly Mock<IAlbumRepository> _albumRepositoryMock;
        private readonly Mock<IImageUploadService> _imageUploadServiceMock;

        public UploadPhotoCommandHandlerTest()
        {
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

            _albumRepositoryMock.Setup(p => p.FindAsync(It.IsAny<Guid>())).ReturnsAsync(album);

            _albumRepositoryMock.Setup(p => p.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var handler = new UploadPhotoCommandHandler(_albumRepositoryMock.Object, _imageUploadServiceMock.Object);

            var result = await handler.Handle(uploadPhotoCmd, default);

            Assert.True(result);
        }

        [Fact]
        public async void Handle_ShouldReturnFalse_GivenInvalidCommand()
        {
            var f = new Faker();

            var album = AlbumFactory.Create();

            var uploadPhotoCmd = new UploadPhotoCommand(new Guid(), null, null, null);

            _imageUploadServiceMock.Setup(s => s.UploadAsync(It.IsAny<IFormFile>())).ReturnsAsync(new System.Uri(new Faker().Internet.UrlWithPath()));

            _albumRepositoryMock.Setup(p => p.FindAsync(It.IsAny<Guid>())).ReturnsAsync(album);

            _albumRepositoryMock.Setup(p => p.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var handler = new UploadPhotoCommandHandler(_albumRepositoryMock.Object, _imageUploadServiceMock.Object);

            var result = await handler.Handle(uploadPhotoCmd, default);

            Assert.False(result);
        }

        [Fact]
        public async void Handle_ShouldReturnFalse_WhenFindAsyncReturnsNull()
        {
            var f = new Faker();

            var uploadPhotoCmd = new UploadPhotoCommand(new Guid(), f.Lorem.Word(), null, f.Lorem.Sentence());

            _imageUploadServiceMock.Setup(s => s.UploadAsync(It.IsAny<IFormFile>())).ReturnsAsync(new System.Uri(new Faker().Internet.UrlWithPath()));

            _albumRepositoryMock.Setup(p => p.FindAsync(It.IsAny<Guid>())).ReturnsAsync(null as Album);

            _albumRepositoryMock.Setup(p => p.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var handler = new UploadPhotoCommandHandler(_albumRepositoryMock.Object, _imageUploadServiceMock.Object);

            var result = await handler.Handle(uploadPhotoCmd, default);

            Assert.False(result);
        }
    }
}