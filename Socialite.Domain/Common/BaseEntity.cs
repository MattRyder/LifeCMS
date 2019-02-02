using System.Collections.Generic;

namespace Socialite.Domain.Common
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// ID for this BaseEntity
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// List of Domain Events being raised for this BaseEntity
        /// </summary>
        private List<BaseEvent> _events;
        public IReadOnlyCollection<BaseEvent> Events => _events?.AsReadOnly();

        public void AddEvent(BaseEvent eventItem)
        {
            _events = _events ?? new List<BaseEvent>();
            _events.Add(eventItem);
        }

        public void RemoveEvent(BaseEvent eventItem)
        {
            if (_events is null) return;
            _events.Remove(eventItem);
        }

        public void ClearEvents()
        {
            if (_events is null) return;
            _events.Clear();
        }
    }
}
