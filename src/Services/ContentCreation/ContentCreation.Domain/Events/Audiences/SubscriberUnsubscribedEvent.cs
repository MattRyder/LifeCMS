using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.Events.Audiences
{
    public class SubscriberUnsubscribedEvent : BaseEvent
    {
        public Subscriber Subscriber { get; private set; }

        public SubscriberUnsubscribedEvent(Subscriber subscriber)
        {
            Subscriber = subscriber;
        }
    }
}
