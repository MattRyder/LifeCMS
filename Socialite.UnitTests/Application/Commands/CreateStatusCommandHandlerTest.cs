using MediatR;
using Moq;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Xunit;
using System.Threading.Tasks;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Commands.Statuses;
using System.Threading;

namespace Socialite.UnitTests.Application.Commands
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

            var createStatusCommand = new CreateStatusCommand(status.Mood, status.Text);

            _statusRepositoryMock.Setup(statusRepo => statusRepo.Add(It.IsAny<Status>())).Returns(status);

            _statusRepositoryMock.Setup(statusRepo => statusRepo.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var handler = new CreateStatusCommandHandler(_statusRepositoryMock.Object);

            var result = await handler.Handle(createStatusCommand, default(CancellationToken));

            Assert.True(result);
        }
    }
}