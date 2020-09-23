using System;
using System.Threading.Tasks;
using LifeCMS.Services.Identity.API.IntegrationEvents;
using Scriban;

namespace LifeCMS.Services.Identity.API.Services.PasswordResetConfirmedEmailService
{
    public class PasswordResetConfirmedEmailServiceException : Exception
    {
        public PasswordResetConfirmedEmailServiceException(string message)
            : base(message) { }
    }

    public class PasswordResetConfirmedEmailService
    {
        private readonly string _emailAddress;

        public PasswordResetConfirmedEmailService(
            string emailAddress)
        {
            _emailAddress = emailAddress ??
                throw new PasswordResetConfirmedEmailServiceException(nameof(emailAddress));
        }

        public async Task<SendEmailEvent> CreateIntegrationEventAsync()
        {
            var body = await CreateEmailBodyTextAsync(_emailAddress);

            return new SendEmailEvent()
            {
                From = PasswordResetConfirmedEmailTemplate.From,
                To = new[] { _emailAddress },
                Subject = PasswordResetConfirmedEmailTemplate.Subject,
                Body = body
            };
        }

        private static async Task<string> CreateEmailBodyTextAsync(
            string emailAddress
        )
        {
            return await Template
                .ParseLiquid(PasswordResetConfirmedEmailTemplate.Template)
                .RenderAsync(new
                {
                    EmailAddress = emailAddress,
                });
        }
    }
}
