using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Domain.Common;

namespace Socialite.Domain.Events.Statuses
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