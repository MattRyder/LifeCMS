using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Socialite.Infrastructure.Identity;
using Socialite.Infrastructure.Responses;

namespace Socialite.Authentication.Application.Commands.Identity
{
    public class CreateIdentityUserCommandHandler : IRequestHandler<CreateIdentityUserCommand, BasicResponse>
    {
        private readonly UserManager<SocialiteIdentityUser> _userManager;

        public CreateIdentityUserCommandHandler(UserManager<SocialiteIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BasicResponse> Handle(CreateIdentityUserCommand request, CancellationToken cancellationToken)
        {
            var user = new SocialiteIdentityUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new BasicResponse()
                {
                    Success = true,
                    Data = new { user.Id }
                };
            }
            else
            {
                return new BasicResponse()
                {
                    Success = false,
                    Errors = result.Errors.ToList().Select((identityError) => identityError.Description),
                };
            }

        }
    }
}