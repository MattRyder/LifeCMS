using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Socialite.Infrastructure.Identity;

namespace Socialite.WebAPI.Application.Commands.Identity
{
    public class CreateIdentityUserCommandHandler : IRequestHandler<CreateIdentityUserCommand, bool>
    {
        private readonly UserManager<SocialiteIdentityUser> _userManager;

        public CreateIdentityUserCommandHandler(UserManager<SocialiteIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(CreateIdentityUserCommand request, CancellationToken cancellationToken)
        {
            var user = new SocialiteIdentityUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            return result.Succeeded;
        }
    }
}