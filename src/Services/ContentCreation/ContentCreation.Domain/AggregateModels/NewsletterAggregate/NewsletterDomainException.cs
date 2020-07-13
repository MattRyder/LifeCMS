using System;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate
{
    public class NewsletterDomainException : Exception
    {
        public NewsletterDomainException(string message) : base(message) { }
    }
}