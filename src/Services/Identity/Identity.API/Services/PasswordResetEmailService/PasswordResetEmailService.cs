using System;
using System.Threading.Tasks;
using LifeCMS.EventBus.IntegrationEvents.Email;
using Scriban;

namespace LifeCMS.Services.Identity.API.Services.PasswordResetEmailService
{
    public class PasswordResetEmailServiceException : Exception
    {
        public PasswordResetEmailServiceException(string message)
            : base(message) { }
    }

    public class PasswordResetEmailService
    {
        private readonly string _identityApiHost;

        private readonly string _fromEmailAddress;

        private readonly string _emailAddress;

        private readonly string _token;

        public PasswordResetEmailService(
            string identityApiHost,
            string fromEmailAddress,
            string emailAddress,
            string token)
        {
            _identityApiHost = identityApiHost ??
                throw new PasswordResetEmailServiceException(nameof(identityApiHost));

            _fromEmailAddress = fromEmailAddress ??
                throw new PasswordResetEmailServiceException(nameof(fromEmailAddress));

            _emailAddress = emailAddress ??
                throw new PasswordResetEmailServiceException(nameof(emailAddress));

            _token = token ??
                throw new PasswordResetEmailServiceException(nameof(token));
        }

        public async Task<SendEmailEvent> CreateIntegrationEventAsync()
        {
            var body = await CreateEmailBodyTextAsync(
                _identityApiHost,
                _emailAddress,
                _token
            );

            return new SendEmailEvent()
            {
                From = _fromEmailAddress,
                To = new[] { _emailAddress },
                Subject = PasswordResetEmailTemplate.Subject,
                Body = body
            };
        }

        private static async Task<string> CreateEmailBodyTextAsync(
            string identityApiHost,
            string emailAddress,
            string token
        )
        {
            return await Template
                .ParseLiquid(PasswordResetEmailTemplate.Template)
                .RenderAsync(new
                {
                    IdentityApiHost = identityApiHost,
                    EmailAddress = emailAddress,
                    Token = token,
                });
        }
    }
}
