using LifeCMS.Services.ContentCreation.Domain.AggregateModels.StatusAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.Events.Statuses
{
    /// <summary>
    /// Raised when a Status is published publically by a user
    /// </summary>
    public class StatusCreatedEvent : BaseEvent
    {
        public Status Status { get; private set; }

        public StatusCreatedEvent(Status status)
        {
            Status = status;
        }
    }
}