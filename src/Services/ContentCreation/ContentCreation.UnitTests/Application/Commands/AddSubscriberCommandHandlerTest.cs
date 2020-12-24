using System;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Audiences;
using LifeCMS.Services.ContentCreation.API.Services.Audiences;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Application.Commands
{
    public class AddSubscriberCommandHandlerTest
    {
        private readonly Mock<IAudienceRepository> _audienceRepositoryMock;

        private readonly Mock<IAudienceLookupService> _audienceLookupServiceMock;

        private readonly Mock<ILogger<AddSubscriberCommandHandler>> _loggerMock;

        private readonly Mock<ISubscriberEmailService> _subscriberEmailServiceMock;

        public AddSubscriberCommandHandlerTest()
        {
            _audienceRepositoryMock = new Mock<IAudienceRepository>();

            _audienceLookupServiceMock = new Mock<IAudienceLookupService>();

            _loggerMock = new Mock<ILogger<AddSubscriberCommandHandler>>();

            _subscriberEmailServiceMock = new Mock<ISubscriberEmailService>();
        }

        [Fact]
        public async void Handle_ShouldReturnTrue_GivenValidCommand()
        {
            var userId = Guid.NewGuid();

            var audience = AudienceFactory.Create(
                userId
            );

            var command = new AddSubscriberCommand(
                audience.Id,
                "Handle_ShouldReturnTrue_GivenValidCommand",
                "Handle_ShouldReturnTrue_GivenValidCommand@example.com",
                false
            );

            _audienceLookupServiceMock
                .Setup(x => x.FindAudienceAsync(audience.Id))
                .ReturnsAsync(audience);

            _audienceRepositoryMock
                .Setup(x => x.UnitOfWork.SaveEntitiesAsync())
                .ReturnsAsync(true);

            var handler = BuildHandler();

            var result = await handler.Handle(command, default);

            Assert.True(result.Success);

            Assert.Equal(1, audience.Subscribers.Count);
        }

        [Fact]
        public async void Handle_ShouldReturnFalse_GivenInvalidAudience()
        {
            var audienceId = Guid.NewGuid();

            var name = "Handle_ShouldReturnFalse_GivenInvalidAudience";

            var command = new AddSubscriberCommand(
                audienceId,
                name,
                "Handle_ShouldReturnFalse_GivenInvalidAudience@example.com",
                true
            );

            _audienceLookupServiceMock
                .Setup(x => x.FindAudienceAsync(audienceId))
                .ThrowsAsync(new AudienceLookupServiceException(""));

            var handler = BuildHandler();

            var result = await handler.Handle(command, default);

            Assert.False(result.Success);
        }

        private AddSubscriberCommandHandler BuildHandler()
        {
            return new AddSubscriberCommandHandler(
                _audienceRepositoryMock.Object,
                _audienceLookupServiceMock.Object,
                _loggerMock.Object,
                _subscriberEmailServiceMock.Object);
        }
    }
}
