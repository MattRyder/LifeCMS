using System.ComponentModel.DataAnnotations;
using MediatR;

namespace LifeCMS.Services.Identity.API.Application.Commands.Password
{
    public class RequestPasswordResetCommand : IRequest<bool>
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; private set; }

        public RequestPasswordResetCommand(string emailAddress)
        {
            EmailAddress = emailAddress;
        }
    }
}
