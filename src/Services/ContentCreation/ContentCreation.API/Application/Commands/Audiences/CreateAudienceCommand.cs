using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Audiences
{
    public class CreateAudienceCommand : IRequest<BasicResponse>
    {
        [Required]
        public string Name { get; private set; }

        public CreateAudienceCommand(string name)
        {
            Name = name;
        }
    }
}
