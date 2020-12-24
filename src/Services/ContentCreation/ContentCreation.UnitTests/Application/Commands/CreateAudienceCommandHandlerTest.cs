using System;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Audiences;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Application.Commands
{
    public class CreateAudienceCommandHandlerTest
    {
        private readonly Mock<IAudienceRepository> _audienceRepositoryMock;

        private readonly Mock<ILogger<CreateAudienceCommandHandler>> _loggerMock;

        private readonly Mock<IUserAccessor> _userAccessorMock;

        public CreateAudienceCommandHandlerTest()
        {
            _audienceRepositoryMock = new Mock<IAudienceRepository>();

            _loggerMock = new Mock<ILogger<CreateAudienceCommandHandler>>();

            _userAccessorMock = new Mock<IUserAccessor>();
        }

        [Fact]
        public async void Handle_ShouldReturnTrue_GivenValidCommand()
        {
            var userId = Guid.NewGuid();

            var audience = AudienceFactory.Create(userId);

            var command = new CreateAudienceCommand(
                name: "Handle_ShouldReturnBasicResponse_GivenValidCommand"
            );

            _audienceRepositoryMock.Setup(x => x.AddAsync(audience));

            _audienceRepositoryMock
                .Setup(x => x.UnitOfWork.SaveEntitiesAsync())
                .ReturnsAsync(true);

            var handler = BuildHandler();

            var result = await handler.Handle(command, default);

            Assert.True(result.Success);
        }

        [Fact]
        public async void Handle_ShouldReturnFalse_WhenHandlerThrows()
        {
            var command = new CreateAudienceCommand(
                name: "CreateAudienceCommandHandlerTest"
            );

            _audienceRepositoryMock
                .Setup(p => p.AddAsync(It.IsAny<Audience>()))
                .Throws(new AudienceDomainException("Handle_ShouldReturnFalse_WhenValidationServiceThrows"));

            var handler = BuildHandler();

            var result = await handler.Handle(command, default);

            Assert.False(result.Success);
        }

        private CreateAudienceCommandHandler BuildHandler()
        {
            return new CreateAudienceCommandHandler(
                _audienceRepositoryMock.Object,
                _loggerMock.Object,
                _userAccessorMock.Object
            );
        }
    }

}
