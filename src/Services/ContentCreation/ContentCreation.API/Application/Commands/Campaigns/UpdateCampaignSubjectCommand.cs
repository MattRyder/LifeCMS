using System;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Campaigns
{
    public class UpdateCampaignSubjectCommand : IRequest<bool>
    {
        public Guid CampaignId { get; private set; }

        public Subject Subject { get; private set; }

        public UpdateCampaignSubjectCommand(Guid campaignId, Subject subject)
        {
            CampaignId = campaignId;

            Subject = subject ?? throw new CampaignDomainException(nameof(subject));
        }
    }
}