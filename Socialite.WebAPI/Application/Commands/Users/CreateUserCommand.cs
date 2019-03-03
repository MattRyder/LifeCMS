using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Socialite.WebAPI.Application.Commands.Users
{
    public class CreateUserCommand : IRequest<bool>
    {
        [Required]
        [EmailAddress]
        public string Email { get; private set; }

        [Required]
        public string Name { get; private set; }

        public CreateUserCommand(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}