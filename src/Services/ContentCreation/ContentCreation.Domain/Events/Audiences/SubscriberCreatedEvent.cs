using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.Events.Audiences
{
    public class SubscriberCreatedEvent : BaseEvent
    {
        public Audience Audience { get; private set; }

        public Subscriber Subscriber { get; private set; }

        public SubscriberCreatedEvent(Audience audience, Subscriber subscriber)
        {
            Audience = audience;

            Subscriber = subscriber;
        }
    }
}
