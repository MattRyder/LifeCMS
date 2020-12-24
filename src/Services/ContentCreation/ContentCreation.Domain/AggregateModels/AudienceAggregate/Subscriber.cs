using System;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Domain.Events.Audiences;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate
{
    public class Subscriber : BaseEntity
    {
        public Guid AudienceId { get; private set; }

        public string Name { get; private set; }

        public EmailAddress EmailAddress { get; private set; }

        public DateTime? SubscribedAt { get; private set; }

        public string SubscribedIpAddress { get; private set; }

        public string SubscriberToken { get; private set; }

        public DateTime? UnsubscribedAt { get; private set; }

        public string UnsubscribedIpAddress { get; private set; }

        private Subscriber(
            Guid audienceId,
            string name,
            DateTime? subscribedAt,
            string subscribedIpAddress,
            string subscriberToken,
            DateTime? unsubscribedAt,
            string unsubscribedIpAddress)
        {
            AudienceId = audienceId;

            Name = name;

            SubscribedAt = subscribedAt;

            SubscribedIpAddress = subscribedIpAddress;

            SubscriberToken = subscriberToken ??
                throw new AudienceDomainException(nameof(subscriberToken));

            UnsubscribedAt = unsubscribedAt;

            UnsubscribedIpAddress = unsubscribedIpAddress;
        }

        public Subscriber(
          Guid audienceId,
            string name,
            EmailAddress emailAddress,
            string subscribedToken,
            DateTime? subscribedAt,
            string subscribedIpAddress,
            DateTime? unsubscribedAt,
            string unsubscribedIpAddress
        ) : this(
            audienceId,
            name,
            subscribedAt,
            subscribedIpAddress,
            subscribedToken,
            unsubscribedAt,
            unsubscribedIpAddress)
        {
            EmailAddress = emailAddress ??
                throw new AudienceDomainException(nameof(emailAddress));
        }

        public void Subscribe(string subscribedIpAddress)
        {
            if (SubscribedAt != null)
            {
                throw new AudienceDomainException("This subscriber has already been subscribed.");
            }

            SubscribedAt = DateTime.Now;

            SubscribedIpAddress = subscribedIpAddress ??
                throw new AudienceDomainException(nameof(subscribedIpAddress));

            AddEvent(new SubscriberSubscribedEvent(this));
        }

        public void Unsubscribe(string unsubscribedIpAddress)
        {
            if (UnsubscribedAt != null)
            {
                throw new AudienceDomainException("This subscriber has already been unsubscribed.");
            }

            UnsubscribedAt = DateTime.Now;

            UnsubscribedIpAddress = unsubscribedIpAddress ??
                throw new AudienceDomainException(nameof(unsubscribedIpAddress));

            AddEvent(new SubscriberUnsubscribedEvent(this));
        }
    }
}
