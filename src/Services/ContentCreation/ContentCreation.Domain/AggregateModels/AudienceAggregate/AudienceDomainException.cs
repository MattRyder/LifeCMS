using System;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate
{
    public class AudienceDomainException : Exception
    {
        public AudienceDomainException()
        {
        }

        public AudienceDomainException(string message) : base(message)
        {
        }
    }
}
