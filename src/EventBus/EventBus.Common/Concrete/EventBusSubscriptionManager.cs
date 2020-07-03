using System;
using System.Collections.Generic;
using System.Linq;
using LifeCMS.EventBus.Common.Events;
using LifeCMS.EventBus.Common.Interfaces;

namespace LifeCMS.EventBus.Common.Concrete
{
    public class EventBusSubscriptionManager : IEventBusSubscriptionManager
    {
        private readonly Dictionary<Type, List<Type>> _eventTypeHandlers;

        public EventBusSubscriptionManager()
        {
            _eventTypeHandlers = new Dictionary<Type, List<Type>>();
        }

        public void AddEventHandler<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            if (HasEventHandler<T, TH>())
            {
                throw new ArgumentException(
                    $@"The event handler '{typeof(TH).Name}' is already subscribed
                        to the event '{GetEventKey<T>()}', please remove it to add a new subscription."
                );
            }

            if (_eventTypeHandlers.ContainsKey(typeof(T)))
            {
                _eventTypeHandlers[typeof(T)].Add(typeof(TH));
            }
            else
            {
                _eventTypeHandlers[typeof(T)] = new List<Type>();

                AddEventHandler<T, TH>();
            }
        }

        public void RemoveEventHandler<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            _eventTypeHandlers[typeof(T)].Remove(typeof(TH));
        }

        public bool HasEvent<T>()
            where T : IntegrationEvent
        {
            return _eventTypeHandlers.Keys.Contains(typeof(T));
        }

        public List<Type> GetEventHandlers<T>()
            where T : IntegrationEvent
        {
            return HasEvent<T>() ? _eventTypeHandlers[typeof(T)] : null;
        }

        public Type GetEventTypeForName(string eventName)
        {
            return _eventTypeHandlers.Keys.First(t => t.Name.Equals(eventName));
        }

        public bool HasEventHandler<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var hasEvent = HasEvent<T>();

            if (hasEvent)
            {
                var eventHandlers = GetEventHandlers<T>();

                return eventHandlers != null && eventHandlers.Contains(typeof(TH));
            }

            return HasEvent<T>() && _eventTypeHandlers[typeof(T)] != null;
        }

        public bool HasEventWithName(string eventName)
        {
            return GetEventTypeForName(eventName) != null;
        }

        public List<Type> GetEventHandlersWithName(string eventName)
        {
            return _eventTypeHandlers[GetEventTypeForName(eventName)];
        }

        public string GetEventKey<T>() => typeof(T).Name;
    }
}
