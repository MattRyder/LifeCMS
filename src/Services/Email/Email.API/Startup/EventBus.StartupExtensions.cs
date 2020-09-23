using LifeCMS.EventBus.Common.Interfaces;
using LifeCMS.EventBus.RabbitMQ.Interfaces;
using LifeCMS.EventBus.RabbitMQ.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using LifeCMS.Services.Email.API.IntegrationEvents;
using LifeCMS.EventBus.Common.Concrete;
using Microsoft.AspNetCore.Builder;

namespace LifeCMS.Services.Email.API
{
    public static partial class StartupExtensions
    {
        public static void AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPersistentConnection>(provider =>
            {
                var logger = provider.GetRequiredService(
                    typeof(ILogger<PersistentConnection>)
                ) as ILogger<PersistentConnection>;

                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBus:HostName"],
                    UserName = configuration["EventBus:Username"],
                    Password = configuration["EventBus:Password"],
                    DispatchConsumersAsync = true,
                };

                return new PersistentConnection(factory, logger);
            });

            services.AddSingleton<IEventBus, RabbitMQEventBus>(provider =>
            {
                var connection = provider
                    .GetRequiredService<IPersistentConnection>();

                var subscriptionManager = provider
                    .GetRequiredService<IEventBusSubscriptionManager>();

                var logger = provider
                    .GetRequiredService<ILogger<RabbitMQEventBus>>();

                return new RabbitMQEventBus(
                    provider,
                    connection,
                    subscriptionManager,
                    logger,
                    queueName: configuration["EventBus:QueueName"]
                );
            });

            services.AddTransient<SendEmailEventHandler>();

            services.AddSingleton<IEventBusSubscriptionManager, EventBusSubscriptionManager>();
        }

        public static void UseEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<SendEmailEvent, SendEmailEventHandler>();
        }
    }
}
