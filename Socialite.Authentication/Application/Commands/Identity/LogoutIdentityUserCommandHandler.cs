using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Socialite.Authentication.Application.Responses;
using Socialite.Infrastructure.Identity;

namespace Socialite.Authentication.Application.Commands.Identity
{
    public class LogoutIdentityUserCommandHandler : IRequestHandler<LogoutIdentityUserCommand, CommandResponse>
    {
        private readonly SignInManager<SocialiteIdentityUser> _signInManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public LogoutIdentityUserCommandHandler(
            SignInManager<SocialiteIdentityUser> signInManager,
            IIdentityServerInteractionService interactionService
        )
        {
            _signInManager = signInManager;

            _interactionService = interactionService;

        }

        public async Task<CommandResponse> Handle(LogoutIdentityUserCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();

            var result = await _interactionService.GetLogoutContextAsync(request.LogoutId);

            return new CommandResponse
            {
                Success = true,
                Data = new { result.PostLogoutRedirectUri },
            };
        }
    }
}