using System;
using System.IO;
using LifeCMS.EventBus.RabbitMQ.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace LifeCMS.EventBus.RabbitMQ.Concrete
{
    public class PersistentConnection : IPersistentConnection
    {
        private bool _isDisposed;

        private IConnection _connection;

        private readonly IConnectionFactory _connectionFactory;

        private readonly ILogger<PersistentConnection> _logger;

        public bool IsConnected => _connection != null && _connection.IsOpen;

        public bool TryConnect()
        {
            _connection = _connectionFactory.CreateConnection();

            if (IsConnected)
            {
                _connection.CallbackException += delegate
                {
                    OnConnectionInterrupted("A PersistentConnection has thrown an exception. Reconnecting now.");
                };

                _connection.ConnectionBlocked += delegate
                {
                    OnConnectionInterrupted("A PersistentConnection is blocked. Reconnecting now.");
                };

                _connection.ConnectionShutdown += delegate
                {
                    OnConnectionInterrupted("A PersistentConnection has shut down. Reconnecting now.");
                };

                _logger.LogInformation($"Connected to event bus '{_connection.Endpoint.HostName}'");
            }
            else
            {
                _logger.LogCritical($"Failed to create PersistentConnection!");
            }

            return IsConnected;
        }

        private void OnConnectionInterrupted(string logMessage)
        {
            if (_isDisposed)
            {
                return;
            }

            _logger.LogWarning(logMessage);

            TryConnect();
        }

        public PersistentConnection(
            IConnectionFactory connectionFactory,
            ILogger<PersistentConnection> logger
        )
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new ArgumentException("You must have an open connection to call CreateModel");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                _logger.LogCritical(null, ex);
            }
        }
    }
}