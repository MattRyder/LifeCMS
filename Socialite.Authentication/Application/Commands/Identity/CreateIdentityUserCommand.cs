using System;
using MediatR;
using Socialite.Authentication.Application.Responses;

namespace Socialite.Authentication.Application.Commands.Identity
{
    public class CreateIdentityUserCommand : IRequest<CommandResponse>
    {
        // [Required]
        // [EmailAddress]
        public string Email { get; set; }

        // [Required]
        public string Password { get; private set; }

        public CreateIdentityUserCommand(string email, string password) : base()
        {
            Email = email;

            Password = password;
        }
    }
}