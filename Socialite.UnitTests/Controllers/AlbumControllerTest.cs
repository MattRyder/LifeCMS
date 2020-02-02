using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Commands.Albums;
using Socialite.WebAPI.Application.Queries.Albums;
using Socialite.WebAPI.Controllers;
using Xunit;

namespace Socialite.UnitTests.Controllers
{
    public class AlbumsControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;

        private readonly Mock<IAlbumQueries> _albumQueriesMock;

        public AlbumsControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();

            _albumQueriesMock = new Mock<IAlbumQueries>();
        }

        [Fact]
        public async void GetAlbums_ReturnsOk()
        {
            IEnumerable<AlbumViewModel> albumList = AlbumFactory.CreateList().ToList().ConvertAll<AlbumViewModel>(p => AlbumViewModel.FromModel(p));

            _albumQueriesMock.Setup(x => x.FindAllAsync()).Returns(Task.FromResult(albumList));

            var controller = new AlbumsController(_mediatorMock.Object, _albumQueriesMock.Object);

            var result = await controller.GetAlbums() as OkObjectResult;

            var resultValue = result.Value as IEnumerable<AlbumViewModel>;

            Assert.NotNull(result);

            Assert.NotNull(resultValue);

            Assert.Equal(albumList, resultValue);
        }

        [Fact]
        public async void Get_ReturnsOk_GivenValidId()
        {
            var album = AlbumViewModel.FromModel(AlbumFactory.Create());

            _albumQueriesMock.Setup(x => x.FindAsync(album.Id)).Returns(Task.FromResult(album));

            var controller = new AlbumsController(_mediatorMock.Object, _albumQueriesMock.Object);

            var result = await controller.GetAlbum(album.Id) as OkObjectResult;

            var resultValue = result.Value as AlbumViewModel;

            Assert.NotNull(result);

            Assert.Equal(album, resultValue);
        }


        [Fact]
        public async void CreateAlbum_ReturnsOk_GivenValidCreateAlbumCommand()
        {
            var command = new CreateAlbumCommand(new Faker().Lorem.Word());

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateAlbumCommand>(), default(CancellationToken))).ReturnsAsync(true);

            var controller = new AlbumsController(_mediatorMock.Object, _albumQueriesMock.Object);

            var result = await controller.CreateAlbum(command) as OkResult;

            Assert.NotNull(result);

            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async void CreateAlbum_ReturnsBadRequest_GivenInalidCreateAlbumCommand()
        {
            var command = new CreateAlbumCommand(new Faker().Lorem.Word());

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateAlbumCommand>(), default(CancellationToken))).ReturnsAsync(false);

            var controller = new AlbumsController(_mediatorMock.Object, _albumQueriesMock.Object);

            var result = await controller.CreateAlbum(command) as BadRequestObjectResult;

            Assert.NotNull(result);

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void UploadPhoto_ReturnsOk_GivenValidUploadPhotoCommand()
        {
            var f = new Faker();

            _mediatorMock.Setup(m => m.Send(It.IsAny<UploadPhotoCommand>(), default)).ReturnsAsync(true);

            var controller = new AlbumsController(_mediatorMock.Object, _albumQueriesMock.Object);

            var result = await controller.UploadPhoto(new Guid(), f.Lorem.Word(), f.Lorem.Sentence(), null) as OkResult;

            Assert.NotNull(result);

            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async void UploadPhoto_ReturnsBadRequest_GivenInalidUploadPhotoCommand()
        {
            var f = new Faker();

            _mediatorMock.Setup(m => m.Send(It.IsAny<UploadPhotoCommand>(), default)).ReturnsAsync(false);

            var controller = new AlbumsController(_mediatorMock.Object, _albumQueriesMock.Object);

            var result = await controller.UploadPhoto(new Guid(), f.Lorem.Word(), f.Lorem.Sentence(), null) as BadRequestObjectResult;

            Assert.NotNull(result);

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}