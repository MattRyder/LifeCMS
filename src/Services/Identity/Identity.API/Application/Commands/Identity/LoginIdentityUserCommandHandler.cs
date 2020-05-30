namespace LifeCMS.Services.Identity.API.Application.Commands.Identity
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using LifeCMS.Services.Identity.Infrastructure;
    using LifeCMS.Services.Identity.Infrastructure.Responses;

    public class LoginIdentityUserCommandHandler : IRequestHandler<LoginIdentityUserCommand, BasicResponse>
    {
        private readonly SignInManager<LifeCMSIdentityUser> _signInManager;

        public LoginIdentityUserCommandHandler(SignInManager<LifeCMSIdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<BasicResponse> Handle(LoginIdentityUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return new BasicResponse()
                {
                    Success = true,
                    Data = request.Return
                };
            }

            return new BasicResponse()
            {
                Success = false,
                Errors = new[] { "Failed to login with those credentials." },
            };
        }
    }
}