using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Files
{
    public class CreateFileUrlCommand : IRequest<BasicResponse>
    {
        [Required]
        public string FileUrn { get; private set; }

        public CreateFileUrlCommand(string fileUrn)
        {
            FileUrn = fileUrn;
        }
    }
}
