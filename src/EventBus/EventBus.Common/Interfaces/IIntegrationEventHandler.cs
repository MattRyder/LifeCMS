using LifeCMS.EventBus.Common.Events;

namespace LifeCMS.EventBus.Common.Interfaces
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        bool Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler { }
}