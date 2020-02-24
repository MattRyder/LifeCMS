using MediatR;
using Socialite.Authentication.Application.Responses;

namespace Socialite.Authentication.Application.Commands.Identity
{
    public class LogoutIdentityUserCommand : IRequest<CommandResponse>
    {
        public string LogoutId { get; set; }
    }
}