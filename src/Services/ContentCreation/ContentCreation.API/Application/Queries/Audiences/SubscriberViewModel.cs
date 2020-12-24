using System;
using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Audiences
{
    public class SubscriberViewModel : ValueObject
    {
        public Guid Id { get; set; }

        public Guid AudienceId { get; set; }

        public string EmailAddress { get; set; }

        public string Name { get; set; }

        public DateTime? SubscribedAt { get; set; }

        public DateTime? UnsubscribedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public static SubscriberViewModel FromModel(Subscriber subscriber)
        {
            return new SubscriberViewModel()
            {
                Id = subscriber.Id,
                AudienceId = subscriber.AudienceId,
                Name = subscriber.Name,
                SubscribedAt = subscriber.SubscribedAt,
                UnsubscribedAt = subscriber.UnsubscribedAt,
                CreatedAt = subscriber.CreatedAt,
                UpdatedAt = subscriber.UpdatedAt,
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return AudienceId;
            yield return SubscribedAt;
            yield return UnsubscribedAt;
            yield return CreatedAt;
            yield return UpdatedAt;
        }
    }
}
