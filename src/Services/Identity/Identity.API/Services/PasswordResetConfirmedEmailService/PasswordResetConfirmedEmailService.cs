using System;
using System.Threading.Tasks;
using LifeCMS.EventBus.IntegrationEvents.Email;
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
        private readonly string _fromEmailAddress;

        private readonly string _toEmailAddress;

        public PasswordResetConfirmedEmailService(
            string fromEmailAddress,
            string toEmailAddress)
        {
            _fromEmailAddress = fromEmailAddress ??
                throw new PasswordResetConfirmedEmailServiceException(nameof(fromEmailAddress));

            _toEmailAddress = toEmailAddress ??
                throw new PasswordResetConfirmedEmailServiceException(nameof(toEmailAddress));
        }

        public async Task<SendEmailEvent> CreateIntegrationEventAsync()
        {
            var body = await CreateEmailBodyTextAsync(_toEmailAddress);

            return new SendEmailEvent()
            {
                From = _fromEmailAddress,
                To = new[] { _toEmailAddress },
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
