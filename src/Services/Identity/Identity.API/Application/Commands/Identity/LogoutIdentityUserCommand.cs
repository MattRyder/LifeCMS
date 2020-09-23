using LifeCMS.Services.Identity.Infrastructure.Responses;
using MediatR;

namespace LifeCMS.Services.Identity.API.Application.Commands.Identity
{
    public class LogoutIdentityUserCommand : IRequest<BasicResponse>
    {
        public string LogoutId { get; set; }
    }
}