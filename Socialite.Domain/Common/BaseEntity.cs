using System.Collections.Generic;

namespace Socialite.Domain.Common
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// ID for this BaseEntity
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// List of Domain Events being raised for this BaseEntity
        /// </summary>
        public List<BaseEvent> Events { get; private set; }

        public void AddEvent(BaseEvent eventItem)
        {
            Events = Events ?? new List<BaseEvent>();
            Events.Add(eventItem);
        }

        public void RemoveEvent(BaseEvent eventItem)
        {
            if (Events is null) return;
            Events.Remove(eventItem);
        }
    }
}
