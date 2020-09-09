using System;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Campaigns;
using LifeCMS.Services.ContentCreation.API.Services.Campaigns;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Application.Commands
{
    public class UpdateCampaignSubjectCommandHandlerTest
    {
        private readonly Mock<ICampaignRepository> _campaignRepositoryMock;

        private readonly Mock<ILogger<UpdateCampaignSubjectCommandHandler>> _loggerMock;

        private readonly Mock<ICampaignLookupService> _campaignLookupService;

        public UpdateCampaignSubjectCommandHandlerTest()
        {
            _campaignRepositoryMock = new Mock<ICampaignRepository>();

            _loggerMock = new Mock<ILogger<UpdateCampaignSubjectCommandHandler>>();

            _campaignLookupService = new Mock<ICampaignLookupService>();
        }

        [Fact]
        public async void Handle_ShouldReturnTrue_GivenValidCommand()
        {
            var campaign = CampaignFactory.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            );

            campaign.Id = Guid.NewGuid();

            var subject = new Subject("A Test Subject", "A Test Preview Text");

            var command = new UpdateCampaignSubjectCommand(campaign.Id, subject);

            _campaignLookupService
                .Setup(ls => ls.FindCampaign(campaign.Id))
                .ReturnsAsync(campaign);

            _campaignRepositoryMock
                .Setup(p => p.UnitOfWork.SaveEntitiesAsync())
                .ReturnsAsync(true);

            var handler = BuildHandler();

            var result = await handler.Handle(command, default);

            Mock.Verify(_campaignRepositoryMock);

            Assert.True(result);
        }

        [Fact]
        public async void Handle_ShouldReturnFalse_GivenInvalidCampaignId()
        {
            var campaignId = Guid.NewGuid();

            var subject = new Subject("A Test Subject", "A Test Preview Text");

            var command = new UpdateCampaignSubjectCommand(campaignId, subject);

            _campaignLookupService
                .Setup(ls => ls.FindCampaign(campaignId))
                .ThrowsAsync(new CampaignLookupServiceException(""));

            var handler = BuildHandler();

            var result = await handler.Handle(command, default);

            Assert.False(result);
        }

        private UpdateCampaignSubjectCommandHandler BuildHandler()
        {
            return new UpdateCampaignSubjectCommandHandler(
                _campaignRepositoryMock.Object,
                _loggerMock.Object,
                _campaignLookupService.Object
            );
        }
    }

}