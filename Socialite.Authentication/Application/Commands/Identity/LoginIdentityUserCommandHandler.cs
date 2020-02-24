namespace Socialite.WebAPI.Application.Commands.Identity
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Socialite.Authentication.Application.Commands.Identity;
    using Socialite.Authentication.Application.Responses;
    using Socialite.Infrastructure.Identity;

    public class LoginIdentityUserCommandHandler : IRequestHandler<LoginIdentityUserCommand, CommandResponse>
    {
        private readonly SignInManager<SocialiteIdentityUser> _signInManager;

        public LoginIdentityUserCommandHandler(SignInManager<SocialiteIdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<CommandResponse> Handle(LoginIdentityUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                    return new CommandResponse()
                    {
                        Success = true,
                        Data = request.ReturnUrl
                    };
            }

            return new CommandResponse()
            {
                Success = false,
                Errors = new[] { "Failed to login with those credentials." },
            };
        }
    }
}