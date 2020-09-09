using System;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Campaigns
{
    public class UpdateCampaignNameDTO
    {
        public string Name { get; set; }
    }

    public class UpdateCampaignNameCommand : IRequest<bool>
    {
        public Guid CampaignId { get; private set; }

        public string Name { get; private set; }

        public UpdateCampaignNameCommand(Guid campaignId, string name)
        {
            CampaignId = campaignId;

            Name = name ?? throw new CampaignDomainException(nameof(name));
        }
    }
}