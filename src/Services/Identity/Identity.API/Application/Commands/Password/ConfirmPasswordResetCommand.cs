using System.ComponentModel.DataAnnotations;
using MediatR;

namespace LifeCMS.Services.Identity.API.Application.Commands.Password
{
    public class ConfirmPasswordResetCommand : IRequest<bool>
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; private set; }

        [Required]
        public string Token { get; private set; }

        [Required]
        public string NewPassword { get; private set; }

        public ConfirmPasswordResetCommand(
            string emailAddress,
            string token,
            string newPassword)
        {
            EmailAddress = emailAddress;

            Token = token;

            NewPassword = newPassword;
        }
    }
}
