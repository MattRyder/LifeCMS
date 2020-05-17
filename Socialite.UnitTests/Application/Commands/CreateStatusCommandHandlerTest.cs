using MediatR;
using Moq;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Xunit;
using System.Threading.Tasks;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Commands.Statuses;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Socialite.UnitTests.Application.Commands
{
    public class CreateStatusCommandHandlerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IStatusRepository> _statusRepositoryMock;
        private readonly Mock<ILogger<CreateStatusCommandHandler>> _loggerMock;

        public CreateStatusCommandHandlerTest()
        {
            _mediator = new Mock<IMediator>();
            _statusRepositoryMock = new Mock<IStatusRepository>();
            _loggerMock = new Mock<ILogger<CreateStatusCommandHandler>>();
        }

        [Fact]
        public async void Handle_ShouldCreateStatus_GivenValidCommand()
        {
            var status = StatusFactory.Create();

            var createStatusCommand = new CreateStatusCommand(status.Mood, status.Text);

            _statusRepositoryMock.Setup(statusRepo => statusRepo.Add(It.IsAny<Status>())).Returns(status);

            _statusRepositoryMock.Setup(statusRepo => statusRepo.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var handler = new CreateStatusCommandHandler(_statusRepositoryMock.Object, _loggerMock.Object);

            var result = await handler.Handle(createStatusCommand, default);

            Assert.True(result.Success);
        }
    }
}