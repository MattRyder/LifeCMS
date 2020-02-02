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
        public string Password { get; private set; }

        public string ReturnUrl { get; private set; }

        public LoginIdentityUserCommand(string email, string password, string returnUrl)
        {
            Email = email;

            Password = password;

            ReturnUrl = returnUrl;
        }
    }
}