using System.Collections.Generic;
using System.Net;
using System.Threading;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Commands.Users;
using Socialite.WebAPI.Application.Queries.Users;
using Socialite.WebAPI.Controllers;
using Xunit;

namespace Socialite.UnitTests.Controllers
{
    public class UsersControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IUserQueries> _userQueries;

        public UsersControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _userQueries = new Mock<IUserQueries>();
        }

        [Fact]
        public async void CreateUser_ReturnsOk_GivenValidCommand()
        {
            var user = UserFactory.Create();

            var createUserCmd = new CreateUserCommand(user.Email, user.Name);

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), default(CancellationToken))).ReturnsAsync(true);

            var controller = new UsersController(_mediatorMock.Object, _userQueries.Object);

            var result = await controller.CreateUser(createUserCmd) as OkResult;

            Assert.NotNull(result);

            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async void CreateUser_ReturnsBadRequest_GivenInvalidCommand()
        {
            var createUserCmd = new CreateUserCommand(string.Empty, string.Empty);

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), default(CancellationToken))).ReturnsAsync(false);

            var controller = new UsersController(_mediatorMock.Object, _userQueries.Object);

            var result = await controller.CreateUser(createUserCmd) as BadRequestObjectResult;

            var resultValue = result.Value as CreateUserCommand;

            Assert.NotNull(result);

            Assert.NotNull(resultValue);

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);

            Assert.Equal(createUserCmd, resultValue);
        }

        [Fact]
        public async void GetUser_ReturnsOk_GivenValidId()
        {
            var user = UserFactory.Create();

            var viewModel = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                CreatedAt = user.CreatedAt.ToString("o"),
                UpdatedAt = user.UpdatedAt.ToString("o")
            };

            _userQueries.Setup(u => u.FindAsync(It.IsAny<int>())).ReturnsAsync(viewModel);

            var controller = new UsersController(_mediatorMock.Object, _userQueries.Object);

            var result = await controller.GetUser(user.Id) as OkObjectResult;

            var resultValue = result.Value as UserViewModel;

            Assert.NotNull(result);

            Assert.NotNull(resultValue);

            Assert.Equal(viewModel, resultValue);
        }

        [Fact]
        public async void GetUser_ReturnsNotFound_GivenAbsentId()
        {
            var nullViewModel = null as UserViewModel;

            _userQueries.Setup(u => u.FindAsync(It.IsAny<int>())).ThrowsAsync(new KeyNotFoundException());

            var controller = new UsersController(_mediatorMock.Object, _userQueries.Object);

            var result = await controller.GetUser(1) as NotFoundResult;

            Assert.NotNull(result);

            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}