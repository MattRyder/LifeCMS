using System;
using System.Text;
using System.Threading.Tasks;
using LifeCMS.EventBus.Common.Concrete;
using LifeCMS.EventBus.Common.Events;
using LifeCMS.EventBus.Common.Interfaces;
using LifeCMS.EventBus.RabbitMQ.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LifeCMS.EventBus.RabbitMQ.Concrete
{
    public class RabbitMQEventBus : IEventBus
    {
        private const string EXCHANGE_NAME = "lifecms_event_bus";

        private readonly IServiceProvider _serviceProvider;

        private readonly IPersistentConnection _connection;

        private readonly IEventBusSubscriptionManager _subscriptionManager;

        private readonly ILogger<RabbitMQEventBus> _logger;

        private readonly string _queueName;

        private IModel _consumerChannel;

        public RabbitMQEventBus(
            IServiceProvider serviceProvider,
            IPersistentConnection connection,
            IEventBusSubscriptionManager subscriptionManager,
            ILogger<RabbitMQEventBus> logger,
            string queueName = null)
        {
            _serviceProvider = serviceProvider;

            _connection = connection;

            _subscriptionManager = subscriptionManager ?? new EventBusSubscriptionManager();

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _queueName = queueName;

            _consumerChannel = CreateConsumerChannel();
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            var eventName = @event.GetType().Name;

            using var channel = _connection.CreateModel();

            _logger.LogTrace($"Declaring an exchange: {@event.Id}");

            channel.ExchangeDeclare(
                exchange: EXCHANGE_NAME,
                type: ExchangeType.Direct,
                durable: false
            );

            channel.QueueBind(
                exchange: EXCHANGE_NAME,
                queue: _queueName,
                routingKey: eventName,
                arguments: null
            );

            var message = JsonConvert.SerializeObject(@event);

            var body = Encoding.UTF8.GetBytes(message);

            var properties = CreateProperties(channel);

            channel.BasicPublish(
                exchange: EXCHANGE_NAME,
                routingKey: eventName,
                mandatory: true,
                basicProperties: properties,
                body: body
            );
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            DoInternalSubscribe<T, TH>();

            _subscriptionManager.AddEventHandler<T, TH>();

            Consume();
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            _subscriptionManager.RemoveEventHandler<T, TH>();
        }

        private IBasicProperties CreateProperties(IModel channel)
        {
            const byte DeliveryModePersistent = 2;

            var properties = channel.CreateBasicProperties();

            properties.DeliveryMode = DeliveryModePersistent;

            return properties;
        }

        private void DoInternalSubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            if (_subscriptionManager.HasEventHandler<T, TH>())
            {
                _logger.LogError("Failed to do internal RabbitMQ subscribe! Already listening...");

                return;
            }

            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            _logger.LogInformation($"Binding to RabbitMQ Queue '{_queueName}'.");

            using var channel = _connection.CreateModel();

            channel.QueueBind(
                exchange: EXCHANGE_NAME,
                queue: _queueName,
                routingKey: typeof(T).Name
            );
        }

        private IModel CreateConsumerChannel()
        {
            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            _logger.LogInformation("Consumer now connecting to Event Bus.");

            var channel = _connection.CreateModel();

            channel.ExchangeDeclare(
                exchange: EXCHANGE_NAME,
                type: ExchangeType.Direct
            );

            if (!string.IsNullOrEmpty(_queueName))
            {
                channel.QueueDeclare(
                    queue: _queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );
            }

            channel.CallbackException += delegate (object sender, CallbackExceptionEventArgs eventArgs)
            {
                _logger.LogError(eventArgs.Exception, "Callback Exception thrown, recreating channel.");

                _consumerChannel.Dispose();

                _consumerChannel = CreateConsumerChannel();

                Consume();
            };

            return channel;
        }

        private void Consume()
        {
            _logger.LogInformation("RabbitMQ EventBus is starting consumption.");

            if (_consumerChannel == null)
            {
                _logger.LogError("Failed to start consumer, the consumer channel is not initialized.");

                return;
            }

            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

            consumer.Received += OnMessageRecieved;

            _consumerChannel.BasicConsume(
                queue: _queueName,
                autoAck: false,
                consumer: consumer
            );

            _logger.LogInformation("RabbitMQ EventBus is now consuming events.");
        }

        private async Task OnMessageRecieved(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;

            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

            try
            {
                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, ex.Message);
            }

            _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (!_subscriptionManager.HasEventWithName(eventName))
            {
                _logger.LogWarning($"No subscriptions registered for event: '{eventName}'");

                return;
            }

            var eventType = _subscriptionManager.GetEventTypeForName(eventName);

            var eventHandlersList = _subscriptionManager.GetEventHandlersWithName(eventName);

            foreach (var eventHandlerType in eventHandlersList)
            {
                var eventHandler = _serviceProvider.GetService(eventHandlerType);

                if (eventHandler == null)
                {
                    continue;
                }

                var integrationEvent = JsonConvert.DeserializeObject(message, eventType);

                var concreteType = typeof(IIntegrationEventHandler<>)
                    .MakeGenericType(eventType);

                await Task.Yield();

                await (Task)concreteType
                    .GetMethod("Handle")
                    .Invoke(eventHandler, new[] { integrationEvent });
            }
        }
    }
}