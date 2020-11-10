using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using LifeCMS.Services.Email.Infrastructure.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace LifeCMS.Services.Email.Infrastructure.Smtp
{
    public class SendGridEmailClient : IEmailClient
    {
        private readonly string _apiKey;

        private readonly MailAddress _fromEmailAddress;

        public SendGridEmailClient(
            string apiKey,
            MailAddress fromEmailAddress
        )
        {
            _apiKey = apiKey ??
                throw new ArgumentNullException(nameof(apiKey));

            _fromEmailAddress = fromEmailAddress ??
                throw new ArgumentNullException(nameof(fromEmailAddress));
        }

        public async void Send(MailMessage mailMessage)
        {
            var sendGridClient = GetSendGridClient();

            var sendGridMessage = BuildSendGridMessage(mailMessage);

            var response = await sendGridClient
                .SendEmailAsync(sendGridMessage)
                .ConfigureAwait(false);

            ProcessResponse(response);
        }

        private SendGridClient GetSendGridClient()
        {
            return new SendGridClient(_apiKey);
        }

        private void ProcessResponse(Response response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode statusCode
                    when (int)statusCode >= 200 && (int)statusCode <= 299:
                    return;
                case HttpStatusCode statusCode
                    when (int)statusCode >= 400 && (int)statusCode <= 599:
                    var json = response.Body.ReadAsStringAsync().Result;

                    throw new EmailClientException(json);
                default:
                    throw new EmailClientException(
                        $"Unexpected SendGrid response status code: {response.StatusCode}"
                    );
            }
        }

        private List<EmailAddress> GetEmailAddresses(
            IEnumerable<MailAddress> mailAddresses
        ) => mailAddresses
            .Select(e => new EmailAddress(e.Address, e.DisplayName))
            .ToList();

        private void AddRecipients(
            SendGridMessage message,
            MailMessage mailMessage
        )
        {
            message.AddTos(GetEmailAddresses(mailMessage.To));

            message.AddCcs(GetEmailAddresses(mailMessage.CC));

            message.AddBccs(GetEmailAddresses(mailMessage.Bcc));

        }

        private SendGridMessage BuildSendGridMessage(MailMessage mailMessage)
        {
            var sendGridMessage = new SendGridMessage()
            {
                From = new EmailAddress(
                    _fromEmailAddress.Address,
                    _fromEmailAddress.DisplayName
                ),
                Subject = mailMessage.Subject,
                PlainTextContent = mailMessage.Body
            };

            AddRecipients(sendGridMessage, mailMessage);

            return sendGridMessage;
        }
    }
}
