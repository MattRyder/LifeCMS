using MediatR;
using Moq;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.WebAPI.Application.Commands.Status;
using Xunit;
using Bogus;
using System.Threading.Tasks;
using Socialite.UnitTests.Factories;

namespace Socialite.UnitTests.Application
{
    public class CreateStatusCommandHandlerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IStatusRepository> _statusRepositoryMock;

        public CreateStatusCommandHandlerTest()
        {
            _mediator = new Mock<IMediator>();
            _statusRepositoryMock = new Mock<IStatusRepository>();
        }

        [Fact]
        public async void Handle_ShouldCreateStatus_GivenValidCommand()
        {
            var status = StatusFactory.Create();

            var createStatusCommand = new CreateStatusCommand(status);

            _statusRepositoryMock.Setup(statusRepo => statusRepo.Add(status)).Returns(status);

            _statusRepositoryMock.Setup(statusRepo => statusRepo.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var handler = new CreateStatusCommandHandler(_statusRepositoryMock.Object);

            var result = await handler.Handle(createStatusCommand, new System.Threading.CancellationToken());

            Assert.True(result);
        }
    }
}