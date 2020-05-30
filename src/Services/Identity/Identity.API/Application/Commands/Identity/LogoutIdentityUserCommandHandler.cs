using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.Services;
using LifeCMS.Services.Identity.Infrastructure;
using LifeCMS.Services.Identity.Infrastructure.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LifeCMS.Services.Identity.API.Application.Commands.Identity
{
    public class LogoutIdentityUserCommandHandler : IRequestHandler<LogoutIdentityUserCommand, BasicResponse>
    {
        private readonly SignInManager<LifeCMSIdentityUser> _signInManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public LogoutIdentityUserCommandHandler(
            SignInManager<LifeCMSIdentityUser> signInManager,
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