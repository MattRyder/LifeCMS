using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.Identity.Infrastructure.Responses;
using MediatR;

namespace LifeCMS.Services.Identity.API.Application.Commands.Identity
{
    public class LoginIdentityUserCommand : IRequest<BasicResponse>
    {
        [Required]
        [EmailAddress]
        public string Email { get; private set; }

        [Required]
        public string Password { get; private set; }

        [Required]
        public string Return { get; private set; }

        public LoginIdentityUserCommand(string email, string password, string returnurl)
        {
            Email = email;

            Password = password;

            Return = returnurl;
        }
    }
}
