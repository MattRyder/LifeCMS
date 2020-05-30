using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.Identity.Infrastructure.Responses;
using MediatR;

namespace LifeCMS.Services.Identity.API.Application.Commands.Identity
{
    public class CreateIdentityUserCommand : IRequest<BasicResponse>
    {
        [Required]
        [EmailAddress]
        public string Email { get; private set; }

        [Required]
        public string Password { get; private set; }

        public CreateIdentityUserCommand(string email, string password)
        {
            Email = email;

            Password = password;
        }
    }
}
