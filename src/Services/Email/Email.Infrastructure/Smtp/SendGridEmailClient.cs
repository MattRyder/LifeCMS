using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using LifeCMS.Services.Email.Domain.Concrete;
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
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));

            _fromEmailAddress = fromEmailAddress ?? throw new ArgumentNullException(nameof(fromEmailAddress));
        }

        public async void Send(EmailMessage emailMessage)
        {
            var sendGridClient = GetSendGridClient();

            var sendGridMessage = BuildSendGridMessage(emailMessage);

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

        private List<EmailAddress> GetEmailAddresses(IEnumerable<string> emailAddressStrings)
            => emailAddressStrings.Select(e => new EmailAddress(e)).ToList();

        private SendGridMessage BuildSendGridMessage(EmailMessage emailMessage)
        {
            var message = new SendGridMessage()
            {
                From = new EmailAddress(
                    _fromEmailAddress.Address,
                    _fromEmailAddress.DisplayName
                ),
                Subject = emailMessage.Subject,
                PlainTextContent = emailMessage.Body
            };

            if (emailMessage.To.Count() > 0)
            {
                message.AddTos(GetEmailAddresses(emailMessage.To));
            }

            if (emailMessage.Cc.Count() > 0)
            {
                message.AddCcs(GetEmailAddresses(emailMessage.Cc));
            }

            if (emailMessage.Bcc.Count() > 0)
            {
                message.AddBccs(GetEmailAddresses(emailMessage.Bcc));
            }

            return message;
        }
    }
}
