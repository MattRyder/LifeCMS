using System;
using System.Security.Claims;
using System.Threading.Tasks;
using LifeCMS.EventBus.Common.Interfaces;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Campaigns;
using LifeCMS.Services.ContentCreation.API.Services.Campaigns;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Application.Commands
{
    public class CreateCampaignCommandHandlerTest
    {
        private readonly Mock<ICampaignRepository> _campaignRepositoryMock;

        private readonly Mock<ILogger<CreateCampaignCommandHandler>> _loggerMock;

        private readonly Mock<IUserAccessor> _userAccessorMock;

        private readonly Mock<ICampaignValidationService> _campaignValidationServiceMock;

        private readonly Mock<ICampaignDeliveryService> _campaignDeliveryServiceMock;

        public CreateCampaignCommandHandlerTest()
        {
            _campaignRepositoryMock = new Mock<ICampaignRepository>();

            _loggerMock = new Mock<ILogger<CreateCampaignCommandHandler>>();

            _userAccessorMock = new Mock<IUserAccessor>();

            _campaignValidationServiceMock = new Mock<ICampaignValidationService>();

            _campaignDeliveryServiceMock = new Mock<ICampaignDeliveryService>();
        }

        [Fact]
        public async void Handle_ShouldReturnTrue_GivenValidCommand()
        {
            var userId = Guid.NewGuid();

            var newsletterTemplateId = Guid.NewGuid();

            var userProfileId = Guid.NewGuid();

            var campaign = CampaignFactory.Create(
                userId,
                newsletterTemplateId,
                userProfileId
            );

            var command = BuildCommand(campaign);

            _campaignValidationServiceMock
                .Setup(cs => cs.ValidateDependencyOwnership(
                    It.IsAny<ClaimsPrincipal>(),
                    newsletterTemplateId,
                    userProfileId
                ))
                .Returns(Task.FromResult(true));

            _campaignRepositoryMock
                .Setup(p => p.Add(campaign))
                .Returns(campaign);

            _campaignRepositoryMock
                .Setup(p => p.UnitOfWork.SaveEntitiesAsync())
                .ReturnsAsync(true);

            var handler = BuildHandler();

            var result = await handler.Handle(command, default);

            Assert.True(result);
        }

        [Fact]
        public async void Handle_ShouldReturnFalse_WhenValidationServiceThrows()
        {
            var userId = Guid.NewGuid();

            var newsletterTemplateId = Guid.NewGuid();

            var userProfileId = Guid.NewGuid();

            var campaign = CampaignFactory.Create(
                userId,
                newsletterTemplateId,
                userProfileId
            );

            var command = BuildCommand(campaign);

            _campaignValidationServiceMock
                .Setup(v => v.ValidateDependencyOwnership(
                    _userAccessorMock.Object.User,
                    newsletterTemplateId,
                    userProfileId
                ))
                .Throws(new CampaignValidationServiceException(""));

            var handler = BuildHandler();

            var result = await handler.Handle(command, default);

            Assert.False(result);
        }

        private CreateCampaignCommand BuildCommand(
            Campaign campaign
        )
        {
            return new CreateCampaignCommand(
                campaign.NewsletterTemplateId,
                campaign.UserProfileId,
                campaign.Name,
                campaign.Subject,
                campaign.ScheduledDate,
                campaign.UseSubscriberTimeZone
            );
        }

        private CreateCampaignCommandHandler BuildHandler()
        {
            return new CreateCampaignCommandHandler(
                _campaignRepositoryMock.Object,
                _loggerMock.Object,
                _userAccessorMock.Object,
                _campaignValidationServiceMock.Object,
                _campaignDeliveryServiceMock.Object
            );
        }
    }

}
