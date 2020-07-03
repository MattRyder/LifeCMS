using System;
using RabbitMQ.Client;

namespace LifeCMS.EventBus.RabbitMQ.Interfaces
{
    public interface IPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}