using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Socialite.WebAPI.Application.Commands.Identity
{
    public class LoginIdentityUserCommand : IRequest<string>
    {
        // [Required]
        // [EmailAddress]
        public string Email { get; set; }

        // [Required]
        public string Password { get; private set; }

        public LoginIdentityUserCommand(string email, string password)
        {
            Email = email;

            Password = password;
        }
    }
}