using System.Threading;
using System.Threading.Tasks;
using System.Web;
using LifeCMS.EventBus.Common.Interfaces;
using LifeCMS.Services.Identity.API.Services.PasswordResetEmailService;
using LifeCMS.Services.Identity.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.Identity.API.Application.Commands.Password
{
    public class RequestPasswordResetCommandHandler
    : IRequestHandler<RequestPasswordResetCommand, bool>
    {
        private readonly IConfiguration _configuration;

        private readonly UserManager<LifeCMSIdentityUser> _userManager;

        private readonly IEventBus _eventBus;

        private readonly ILogger<RequestPasswordResetCommandHandler> _logger;

        public RequestPasswordResetCommandHandler(
            IConfiguration configuration,
            UserManager<LifeCMSIdentityUser> userManager,
            IEventBus eventBus,
            ILogger<RequestPasswordResetCommandHandler> logger
        )
        {
            _configuration = configuration;

            _userManager = userManager;

            _eventBus = eventBus;

            _logger = logger;
        }

        public async Task<bool> Handle(
            RequestPasswordResetCommand request,
            CancellationToken cancellationToken)
        {
            var identityApiHost = GetIdentityApiHost();

            var fromEmailAddress = GetFromEmailAddress();

            var user = await FindUser(request.EmailAddress);

            if (user == null)
            {
                _logger.LogError("Failed to find a user for the email address.");

                return false;
            }

            var token = await GenerateResetToken(user);

            SendEmail(identityApiHost, fromEmailAddress, user.Email, token);

            return true;
        }

        private string GetIdentityApiHost()
        {
            return _configuration["ApiHost:Identity"];
        }

        private string GetFromEmailAddress()
        {
            return _configuration["Identity:Email:FromEmailAddress"];
        }

        private async Task<LifeCMSIdentityUser> FindUser(string emailAddress)
        {
            return await _userManager.FindByNameAsync(emailAddress);
        }

        private async Task<string> GenerateResetToken(LifeCMSIdentityUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return HttpUtility.UrlEncode(token);
        }

        private async void SendEmail(
            string identityApiHost,
            string fromEmailAddress,
            string emailAddress,
            string token
        )
        {
            var service = new PasswordResetEmailService(
                identityApiHost,
                fromEmailAddress,
                emailAddress,
                token
            );

            var @event = await service.CreateIntegrationEventAsync();

            _eventBus.Publish(@event);
        }
    }
}
