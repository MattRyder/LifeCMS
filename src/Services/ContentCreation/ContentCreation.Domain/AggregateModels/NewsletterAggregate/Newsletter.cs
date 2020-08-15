using System;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Domain.Events.Newsletters;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate
{
    public class Newsletter : BaseEntity, IAggregateRoot
    {
        public Guid UserId { get; private set; }

        public string Name { get; private set; }

        public NewsletterBody Body { get; private set; }

        // A constructor without a value object argument (EmailAddress) is required for EF Core right now
        // https://github.com/dotnet/efcore/issues/12078
        private Newsletter(Guid userId, string name)
        {
            UserId = userId != null ? userId : throw new NewsletterDomainException(nameof(userId));

            Name = name ?? throw new NewsletterDomainException(nameof(name));
        }

        public Newsletter(Guid userId, string name, NewsletterBody body) : this(userId, name)
        {
            Body = body ?? throw new NewsletterDomainException(nameof(body));
        }

        public void UpdateNewsletterBody(NewsletterBody newsletterBody)
        {
            Body = newsletterBody ?? throw new NewsletterDomainException(nameof(newsletterBody));

            AddEvent(new NewsletterBodyUpdatedEvent(this));
        }
    }
}