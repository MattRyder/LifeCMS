using System.Threading;
using System.Threading.Tasks;
using LifeCMS.EventBus.Common.Interfaces;
using LifeCMS.Services.Identity.API.Services.PasswordResetConfirmedEmailService;
using LifeCMS.Services.Identity.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace LifeCMS.Services.Identity.API.Application.Commands.Password
{
    public class ConfirmPasswordResetCommandHandler : IRequestHandler<ConfirmPasswordResetCommand, bool>
    {
        private readonly UserManager<LifeCMSIdentityUser> _userManager;

        private readonly IEventBus _eventBus;

        private readonly IConfiguration _configuration;

        public ConfirmPasswordResetCommandHandler(
            UserManager<LifeCMSIdentityUser> userManager,
            IEventBus eventBus,
            IConfiguration configuration
        )
        {
            _userManager = userManager;

            _eventBus = eventBus;

            _configuration = configuration;
        }


        public async Task<bool> Handle(
            ConfirmPasswordResetCommand request,
            CancellationToken cancellationToken
        )
        {
            var fromEmailAddress = GetFromEmailAddress();

            var user = await FindUserAsync(request.EmailAddress);

            var result = await ResetPasswordAsync(
                user,
                request.Token,
                request.NewPassword
            );

            if (!result)
            {
                return false;
            }

            SendEmailAsync(fromEmailAddress, user.Email);

            return true;
        }

        private async Task<LifeCMSIdentityUser> FindUserAsync(
            string emailAddress
        )
        {
            return await _userManager.FindByEmailAsync(emailAddress);
        }

        private string GetFromEmailAddress()
        {
            return _configuration["Identity:Email:FromEmailAddress"];
        }

        private async Task<bool> ResetPasswordAsync(
            LifeCMSIdentityUser user,
            string token,
            string newPassword
        )
        {
            var result = await _userManager.ResetPasswordAsync(
                user,
                token,
                newPassword
            );

            return result.Succeeded;
        }

        private async void SendEmailAsync(
            string fromEmailAddress,
            string toEmailAddress
        )
        {
            var service = new PasswordResetConfirmedEmailService(
                fromEmailAddress,
                toEmailAddress
            );

            var @event = await service.CreateIntegrationEventAsync();

            _eventBus.Publish(@event);
        }
    }
}
