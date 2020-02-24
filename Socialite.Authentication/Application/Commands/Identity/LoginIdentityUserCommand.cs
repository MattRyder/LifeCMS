using MediatR;
using Socialite.Authentication.Application.Responses;

namespace Socialite.Authentication.Application.Commands.Identity
{
    public class LoginIdentityUserCommand : IRequest<CommandResponse>
    {
        // [Required]
        // [EmailAddress]
        public string Email { get; set; }

        // [Required]
        public string Password { get; set; }

        public bool RememberLogin { get; set; }

        public string ReturnUrl { get; set; }
    }
}