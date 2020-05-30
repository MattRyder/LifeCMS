using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using LifeCMS.Services.Identity.Infrastructure;
using LifeCMS.Services.Identity.Infrastructure.Responses;

namespace LifeCMS.Services.Identity.API.Application.Commands.Identity
{
    public class CreateIdentityUserCommandHandler : IRequestHandler<CreateIdentityUserCommand, BasicResponse>
    {
        private readonly UserManager<LifeCMSIdentityUser> _userManager;

        public CreateIdentityUserCommandHandler(UserManager<LifeCMSIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BasicResponse> Handle(CreateIdentityUserCommand request, CancellationToken cancellationToken)
        {
            var user = new LifeCMSIdentityUser
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