using System.Threading;
using Bogus;
using MediatR;
using Moq;
using Socialite.Domain.AggregateModels.UsersAggregate;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Commands.Users;
using Xunit;

namespace Socialite.UnitTests.Application.Commands
{
    public class CreateUserCommandHandlerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public CreateUserCommandHandlerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact]
        public async void Handle_ShouldCreateUser_GivenValidCommand()
        {
            var email = new Faker().Internet.ExampleEmail();

            var name = new Faker().Name.FullName();

            var createUserCmd = new CreateUserCommand(email, name);

            _userRepositoryMock.Setup(u => u.Add(It.IsAny<User>()));

            _userRepositoryMock.Setup(u => u.UnitOfWork.SaveEntitiesAsync()).ReturnsAsync(true);

            var handler = new CreateUserCommandHandler(_userRepositoryMock.Object);

            var result = await handler.Handle(createUserCmd, default(CancellationToken));

            Assert.True(result);
        }
    }
}