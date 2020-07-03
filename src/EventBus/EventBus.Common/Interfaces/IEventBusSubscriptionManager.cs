using System;
using System.Collections.Generic;
using LifeCMS.EventBus.Common.Events;

namespace LifeCMS.EventBus.Common.Interfaces
{
    public interface IEventBusSubscriptionManager
    {
        void AddEventHandler<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        void RemoveEventHandler<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        bool HasEvent<T>()
            where T : IntegrationEvent;

        bool HasEventHandler<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        bool HasEventWithName(string eventName);

        List<Type> GetEventHandlers<T>()
            where T : IntegrationEvent;

        Type GetEventTypeForName(string eventName);

        List<Type> GetEventHandlersWithName(string eventName);

        string GetEventKey<T>();
    }
}