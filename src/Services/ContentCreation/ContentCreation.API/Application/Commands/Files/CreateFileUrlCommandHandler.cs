using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using LifeCMS.Services.ContentCreation.Infrastructure.Services;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Files
{
    public class CreateFileUrlCommandHandler
        : IRequestHandler<CreateFileUrlCommand, BasicResponse>
    {
        private readonly IFileUriService _fileUriService;

        public CreateFileUrlCommandHandler(IFileUriService fileUriService)
        {
            _fileUriService = fileUriService;
        }

        public Task<BasicResponse> Handle(
            CreateFileUrlCommand request,
            CancellationToken cancellationToken)
        {
            var uri = _fileUriService.GetFileUri(request.FileUrn);

            var response = new BasicResponse()
            {
                Success = true,
                Data = uri,
            };

            return Task.FromResult(response);
        }
    }
}
