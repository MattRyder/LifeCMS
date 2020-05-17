using MediatR;
using Socialite.Infrastructure.Responses;

namespace Socialite.Authentication.Application.Commands.Identity
{
    public class LogoutIdentityUserCommand : IRequest<BasicResponse>
    {
        public string LogoutId { get; set; }
    }
}