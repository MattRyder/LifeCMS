
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.Events.Newsletters
{
    public class NewsletterBodyUpdatedEvent : BaseEvent
    {
        public Newsletter Newsletter { get; private set; }

        public NewsletterBodyUpdatedEvent(Newsletter newsletter)
        {
            Newsletter = newsletter;
        }
    }
}