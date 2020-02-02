using System;
using System.Collections.Generic;

namespace Socialite.Domain.Common
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// ID for this BaseEntity
        /// </summary>
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// List of Domain Events being raised for this BaseEntity
        /// </summary>
        private List<BaseEvent> _events;
        public IReadOnlyCollection<BaseEvent> Events => _events?.AsReadOnly();

        public BaseEntity()
        {
            _events = new List<BaseEvent>();
        }


        /// <summary>
        /// Adds an event to the entities event collection
        /// </summary>
        /// <param name="eventItem">Event to add to the list</param>
        public void AddEvent(BaseEvent eventItem)
        {
            _events.Add(eventItem);
        }

        /// <summary>
        /// Removes an event from the entity event collection
        /// </summary>
        /// <param name="eventItem">Event to remove from the list</param>
        public void RemoveEvent(BaseEvent eventItem)
        {
            _events.Remove(eventItem);
        }

        /// <summary>
        /// Clears all events from the entity
        /// </summary>
        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}
