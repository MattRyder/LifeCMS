using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Socialite.Infrastructure.Identity;
using Socialite.Infrastructure.Responses;

namespace Socialite.Authentication.Application.Commands.Identity
{
    public class LogoutIdentityUserCommandHandler : IRequestHandler<LogoutIdentityUserCommand, BasicResponse>
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

        public async Task<BasicResponse> Handle(LogoutIdentityUserCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();

            var result = await _interactionService.GetLogoutContextAsync(request.LogoutId);

            return new BasicResponse
            {
                Success = true,
                Data = new { result.PostLogoutRedirectUri },
            };
        }
    }
}