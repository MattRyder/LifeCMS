using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.Events.Audiences
{
    public class SubscriberSubscribedEvent : BaseEvent
    {
        public Subscriber Subscriber { get; private set; }

        public SubscriberSubscribedEvent(Subscriber subscriber)
        {
            Subscriber = subscriber;
        }
    }
}
