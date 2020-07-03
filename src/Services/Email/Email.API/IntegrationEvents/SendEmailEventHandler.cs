using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeCMS.EventBus.Common.Interfaces;
using LifeCMS.Services.Email.Domain.Concrete;
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

        public bool Handle(SendEmailEvent @event)
        {
            var message = CreateEmailMessage(@event);

            try
            {
                _emailClient.Send(message);

                return true;
            }
            catch (EmailClientException ex)
            {
                _logger.LogError(ex, ex.Message);

                return false;
            }
        }

        private EmailMessage CreateEmailMessage(SendEmailEvent @event)
        {
            var to = @event.To ?? new List<string>();

            var cc = @event.Cc ?? new List<string>();

            var bcc = @event.Bcc ?? new List<string>();

            return new EmailMessage(
                to: to,
                cc: cc,
                bcc: bcc,
                subject: @event.Subject,
                body: @event.Body
            );
        }
    }
}