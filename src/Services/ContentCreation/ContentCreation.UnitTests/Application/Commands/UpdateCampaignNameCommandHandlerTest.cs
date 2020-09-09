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
    public class UpdateCampaignNameCommandHandlerTest
    {
        private readonly Mock<ICampaignRepository> _campaignRepositoryMock;

        private readonly Mock<ILogger<UpdateCampaignNameCommandHandler>> _loggerMock;

        private readonly Mock<ICampaignLookupService> _campaignLookupService;

        public UpdateCampaignNameCommandHandlerTest()
        {
            _campaignRepositoryMock = new Mock<ICampaignRepository>();

            _loggerMock = new Mock<ILogger<UpdateCampaignNameCommandHandler>>();

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

            var name = "Campaign Name";

            var command = new UpdateCampaignNameCommand(campaign.Id, name);

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

        private UpdateCampaignNameCommandHandler BuildHandler()
        {
            return new UpdateCampaignNameCommandHandler(
                _campaignRepositoryMock.Object,
                _loggerMock.Object,
                _campaignLookupService.Object
            );
        }
    }

}