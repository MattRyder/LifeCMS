using System;
using System.Collections.Generic;
using LifeCMS.EventBus.Common.Events;

namespace LifeCMS.Services.ContentCreation.API.IntegrationEvents
{
    public class SendEmailEvent : IntegrationEvent
    {
        public IEnumerable<string> To { get; private set; }

        public IEnumerable<string> Cc { get; private set; }

        public IEnumerable<string> Bcc { get; private set; }

        public string Subject { get; private set; }

        public string Body { get; private set; }

        public SendEmailEvent(
            IEnumerable<string> to,
            IEnumerable<string> cc,
            IEnumerable<string> bcc,
            string subject,
            string body
        )
        {
            To = to ?? throw new ArgumentNullException(nameof(to));

            Cc = cc ?? default;

            Bcc = bcc ?? default;

            Subject = subject ?? throw new ArgumentNullException(nameof(subject));

            Body = body ?? throw new ArgumentNullException(nameof(body));
        }
    }
}