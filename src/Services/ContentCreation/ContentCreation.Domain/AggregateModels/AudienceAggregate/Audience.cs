using System;
using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Domain.Events.Audiences;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate
{
    public class Audience : BaseEntity, IAggregateRoot
    {
        public Guid UserId { get; private set; }

        public string Name { get; private set; }

        private readonly List<Subscriber> _subscribers;

        public IReadOnlyCollection<Subscriber> Subscribers => _subscribers;

        public Audience(Guid userId, string name)
        {
            _subscribers = new List<Subscriber>();

            UserId = userId;

            Name = name ?? throw new AudienceDomainException(nameof(userId));
        }

        public Subscriber AddSubscriber(
            string name,
            EmailAddress emailAddress,
            string subscriberToken)
        {
            var subscriber = new Subscriber(
                Id,
                name,
                emailAddress,
                subscriberToken,
                null,
                null,
                null,
                null
            );

            _subscribers.Add(subscriber);

            AddEvent(new SubscriberCreatedEvent(this, subscriber));

            return subscriber;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new AudienceDomainException(nameof(name));
            }

            Name = name;
        }
    }
}
