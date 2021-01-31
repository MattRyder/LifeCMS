using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using LifeCMS.Services.ContentCreation.Infrastructure.Services;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Files
{
    public class CreatePresignedPostUrlCommandHandler
    : IRequestHandler<CreatePresignedPostUrlCommand, BasicResponse>
    {
        private readonly IPresignedPostService _presignedPostService;

        public CreatePresignedPostUrlCommandHandler(
            IPresignedPostService presignedPostService)
        {
            _presignedPostService = presignedPostService;
        }

        public Task<BasicResponse> Handle(
            CreatePresignedPostUrlCommand request,
            CancellationToken cancellationToken)
        {
            var presignedPostRequest = _presignedPostService.CreatePresignedUrl(
                request.Filename,
                request.ContentType);

            var response = new BasicResponse()
            {
                Success = true,
                Data = presignedPostRequest,
            };

            return Task.FromResult(response);
        }
    }
}
