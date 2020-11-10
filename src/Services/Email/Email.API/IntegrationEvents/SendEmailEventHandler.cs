using System.Net.Mail;
using System.Linq;
using System.Threading.Tasks;
using LifeCMS.EventBus.Common.Interfaces;
using LifeCMS.EventBus.IntegrationEvents.Email;
using LifeCMS.Services.Email.Infrastructure.Interfaces;
using LifeCMS.Services.Email.Infrastructure.Smtp;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.Email.API.IntegrationEvents
{
    public class SendEmailEventHandler : IIntegrationEventHandler<SendEmailEvent>
    {
        private readonly IEmailClient _emailClient;

        private readonly ILogger<SendEmailEventHandler> _logger;

        public SendEmailEventHandler(
            IEmailClient emailClient,
            ILogger<SendEmailEventHandler> logger
        )
        {
            _emailClient = emailClient;

            _logger = logger;
        }

        public Task<bool> Handle(SendEmailEvent @event)
        {
            try
            {
                var emailMessage = CreateMailMessage(@event);

                _emailClient.Send(emailMessage);

                _logger.LogInformation(
                    $"Email has been sent. Event: {@event.Id}"
                );

                return Task.FromResult(true);
            }
            catch (EmailClientException ex)
            {
                _logger.LogError(ex, ex.Message);

                return Task.FromResult(false);
            }
        }

        private MailMessage CreateMailMessage(SendEmailEvent @event)
        {
            var message = new MailMessage()
            {
                Body = @event.Body,
                From = new MailAddress(@event.From),
                Subject = @event.Subject,
                IsBodyHtml = @event.IsBodyHtml,
            };

            @event.To?.ToList().ForEach((address) => message.To.Add(address));

            @event.Cc?.ToList().ForEach((address) => message.CC.Add(address));

            @event.Bcc?.ToList().ForEach((address) => message.Bcc.Add(address));

            return message;
        }
    }
}
