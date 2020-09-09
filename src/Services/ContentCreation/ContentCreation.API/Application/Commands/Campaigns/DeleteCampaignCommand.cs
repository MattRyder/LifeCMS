using System;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Campaigns
{
    public class DeleteCampaignCommand : IRequest<bool>
    {
        public Guid CampaignId { get; private set; }

        public DeleteCampaignCommand(Guid campaignId)
        {
            CampaignId = campaignId;
        }
    }
}