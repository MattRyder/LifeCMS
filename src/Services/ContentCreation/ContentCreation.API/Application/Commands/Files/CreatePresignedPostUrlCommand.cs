using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Files
{
    public class CreatePresignedPostUrlCommand : IRequest<BasicResponse>
    {
        [Required]
        public string Filename { get; private set; }

        [Required]
        public string ContentType { get; private set; }

        public CreatePresignedPostUrlCommand(
            string filename,
            string contentType)
        {
            Filename = filename;

            ContentType = contentType;
        }
    }
}
