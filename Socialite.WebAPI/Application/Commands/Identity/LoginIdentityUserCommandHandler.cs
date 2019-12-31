namespace Socialite.WebAPI.Application.Commands.Identity
{
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using IdentityServer4.Services;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Socialite.Infrastructure.Identity;
    using Socialite.WebAPI.Authorization.Jwt;

    public class LoginIdentityUserCommandHandler : IRequestHandler<LoginIdentityUserCommand, string>
    {
        private readonly UserManager<SocialiteIdentityUser> _userManager;
        private readonly IJwtGenerationService _jwtGenerationService;

        public LoginIdentityUserCommandHandler(
            UserManager<SocialiteIdentityUser> userManager,
            IJwtGenerationService jwtGenerationService)
        {
            _userManager = userManager;
            _jwtGenerationService = jwtGenerationService;
        }

        public async Task<string> Handle(LoginIdentityUserCommand request, CancellationToken cancellationToken)
        {
            var identity = await GetClaimsIdentity(request.Email, request.Password);

            if (identity == null)
            {
                return null;
            }

            var jwtResponse = _jwtGenerationService.GenerateJwt(
                identity,
                request.Email,
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented
                }
            );

            return jwtResponse;
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            var checkPasswordSuccess = await _userManager.CheckPasswordAsync(user, password);

            if (checkPasswordSuccess)
            {
                var claimsIdentity = _jwtGenerationService.GenerateClaimsIdentity(email, user.Id);

                return await Task.FromResult(claimsIdentity);
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}