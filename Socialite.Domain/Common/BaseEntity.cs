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

        public BaseEntity()
        {
            _events = new List<BaseEvent>();
        }

        public void AddEvent(BaseEvent eventItem)
        {
            _events.Add(eventItem);
        }

        public void RemoveEvent(BaseEvent eventItem)
        {
            _events.Remove(eventItem);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}
