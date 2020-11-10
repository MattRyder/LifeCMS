using System;
using System.Collections.Generic;
using LifeCMS.EventBus.Common.Events;

namespace LifeCMS.EventBus.IntegrationEvents.Email
{
    public class SendEmailEvent : IntegrationEvent
    {
        public string From { get; set; }

        public IEnumerable<string> To { get; set; }

        public IEnumerable<string> Cc { get; set; }

        public IEnumerable<string> Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsBodyHtml { get; set; }

        public DateTime DeliverAt { get; set; }
    }
}
