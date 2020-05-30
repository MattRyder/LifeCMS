using MediatR;
using Moq;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.StatusAggregate;
using Xunit;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Statuses;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;

namespace LifeCMS.Services.ContentCreation.UnitTests.Application.Commands
{
    public class CreateStatusCommandHandlerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IStatusRepository> _statusRepositoryMock;
        private readonly Mock<ILogger<CreateStatusCommandHandler>> _loggerMock;

        public CreateStatusCommandHandlerTest()
        {
            
            _mediatorMock = new Mock<IMediator>();
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

            var handler = new CreateStatusCommandHandler(_statusRepositoryMock.Object, _loggerMock.Object, _mediatorMock.Object);

            var result = await handler.Handle(createStatusCommand, default);

            Assert.True(result.Success);
        }
    }
}