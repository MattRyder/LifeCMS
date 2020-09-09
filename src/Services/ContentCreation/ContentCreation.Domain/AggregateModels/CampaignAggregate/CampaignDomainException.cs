using System;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate
{
    public class CampaignDomainException : Exception
    {
        public CampaignDomainException(string message) : base(message) { }
    }
}