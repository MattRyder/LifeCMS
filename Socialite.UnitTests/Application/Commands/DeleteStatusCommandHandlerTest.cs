using System;
using System.Threading.Tasks;
using Moq;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Commands.Statuses;
using Socialite.WebAPI.Application.Enums;
using Xunit;

namespace Socialite.UnitTests.Application.Commands
{
    public class DeleteStatusCommandHandlerTest
    {
        private readonly Mock<IStatusRepository> _statusRepositoryMock;

        public DeleteStatusCommandHandlerTest()
        {
            _statusRepositoryMock = new Mock<IStatusRepository>();
        }

        [Fact]
        public async void ShouldDeleteStatusAsync()
        {
            var statusId = new Guid();

            var status = StatusFactory.Create();

            _statusRepositoryMock.Setup(s => s.FindAsync(statusId)).ReturnsAsync(status);

            _statusRepositoryMock.Setup(s => s.Delete(It.IsAny<Status>()));

            _statusRepositoryMock.Setup(s => s.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(true));

            var deleteStatusCmd = new DeleteStatusCommand(statusId);

            var handler = new DeleteStatusCommandHandler(_statusRepositoryMock.Object);

            var result = await handler.Handle(deleteStatusCmd, new System.Threading.CancellationToken());

            Assert.Equal(DeleteCommandResult.Success, result);

            _statusRepositoryMock.Verify(x => x.Delete(status), Times.Once);

            _statusRepositoryMock.Verify(x => x.UnitOfWork.SaveEntitiesAsync(), Times.Once);
        }

        [Fact]
        public async void Handle_ReturnsNotFound_GivenAnInvalidStatus()
        {
            var statusId = new Guid();

            _statusRepositoryMock.Setup(s => s.FindAsync(statusId)).Returns(Task.FromResult(null as Status));

            var deleteStatusCmd = new DeleteStatusCommand(statusId);

            var handler = new DeleteStatusCommandHandler(_statusRepositoryMock.Object);

            var result = await handler.Handle(deleteStatusCmd, new System.Threading.CancellationToken());

            Assert.Equal(DeleteCommandResult.NotFound, result);

            _statusRepositoryMock.Verify(x => x.FindAsync(statusId), Times.Once);

            _statusRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Handle_ReturnsFailure()
        {
            var statusId = new Guid();

            var status = StatusFactory.Create();

            _statusRepositoryMock.Setup(s => s.FindAsync(statusId)).Returns(Task.FromResult(status));

            _statusRepositoryMock.Setup(s => s.Delete(status));

            _statusRepositoryMock.Setup(s => s.UnitOfWork.SaveEntitiesAsync()).Returns(Task.FromResult(false));

            var deleteStatusCmd = new DeleteStatusCommand(statusId);

            var handler = new DeleteStatusCommandHandler(_statusRepositoryMock.Object);

            var result = await handler.Handle(deleteStatusCmd, new System.Threading.CancellationToken());

            Assert.Equal(DeleteCommandResult.Failure, result);
        }
    }
}