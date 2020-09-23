using System;
using System.Collections.Generic;

namespace LifeCMS.Services.Email.Domain.Concrete
{
    public class EmailMessage
    {
        public string From { get; private set; }

        public IEnumerable<string> To { get; private set; }

        public IEnumerable<string> Cc { get; private set; }

        public IEnumerable<string> Bcc { get; private set; }

        public string Subject { get; private set; }

        public string Body { get; private set; }

        public EmailMessage(
            string from,
            IEnumerable<string> to,
            IEnumerable<string> cc,
            IEnumerable<string> bcc,
            string subject,
            string body
        )
        {
            From = from ?? throw new ArgumentNullException(nameof(from));

            To = to ?? throw new ArgumentNullException(nameof(to));

            Cc = cc ?? new List<string>();

            Bcc = bcc ?? default;

            Subject = subject ?? throw new ArgumentNullException(nameof(subject));

            Body = body ?? throw new ArgumentNullException(nameof(body));
        }
    }
}
