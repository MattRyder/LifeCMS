using System;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate
{
    public class Campaign : BaseEntity, IAggregateRoot
    {
        public Guid UserId { get; private set; }

        public Guid NewsletterTemplateId { get; private set; }

        public Guid UserProfileId { get; private set; }

        public string Name { get; private set; }

        public Subject Subject { get; private set; }

        public DateTime ScheduledDate { get; private set; }

        public bool UseSubscriberTimeZone { get; private set; }

        // A constructor without a value object argument (Subject) is required for EF Core right now
        // https://github.com/dotnet/efcore/issues/12078
        private Campaign(
            Guid userId,
            Guid newsletterTemplateId,
            Guid userProfileId,
            string name,
            DateTime scheduledDate,
            bool useSubscriberTimeZone
        )
        {
            UserId = userId;
            NewsletterTemplateId = newsletterTemplateId;
            UserProfileId = userProfileId;
            Name = name ?? throw new CampaignDomainException(nameof(name));
            ScheduledDate = scheduledDate;
            UseSubscriberTimeZone = useSubscriberTimeZone;
        }

        public Campaign(
            Guid userId,
            Guid newsletterTemplateId,
            Guid userProfileId,
            string name,
            Subject subject,
            DateTime scheduledDate,
            bool useSubscriberTimeZone
        ) : this(
            userId,
            newsletterTemplateId,
            userProfileId,
            name,
            scheduledDate,
            useSubscriberTimeZone
        )
        {
            Subject = subject ?? throw new CampaignDomainException(nameof(subject));
        }

        public void UpdateName(string name)
        {
            Name = name ?? throw new CampaignDomainException(nameof(name));
        }

        public void UpdateSubject(Subject subject)
        {
            Subject = subject ?? throw new CampaignDomainException(nameof(subject));
        }

        public void UpdateScheduledDate(DateTime scheduledDate)
        {
            ScheduledDate = scheduledDate != null ? scheduledDate : throw new CampaignDomainException(nameof(scheduledDate));
        }

        public void UpdateUseSubscriberTimeZone(bool value)
        {
            UseSubscriberTimeZone = value;
        }
    }
}