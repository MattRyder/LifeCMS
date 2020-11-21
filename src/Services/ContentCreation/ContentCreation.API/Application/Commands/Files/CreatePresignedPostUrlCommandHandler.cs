using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using LifeCMS.Services.ContentCreation.Infrastructure.Services.Aws;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Files
{
    public class CreatePresignedPostUrlCommandHandler
    : IRequestHandler<CreatePresignedPostUrlCommand, BasicResponse>
    {
        private readonly ILogger<CreatePresignedPostUrlCommandHandler> _logger;

        private readonly IPresignedPostService _presignedPostService;

        public CreatePresignedPostUrlCommandHandler(
            ILogger<CreatePresignedPostUrlCommandHandler> logger,
            IPresignedPostService presignedPostService)
        {
            _logger = logger;

            _presignedPostService = presignedPostService;
        }

        public Task<BasicResponse> Handle(
            CreatePresignedPostUrlCommand request,
            CancellationToken cancellationToken)
        {
            var url = _presignedPostService.CreatePresignedUrl(
                request.Filename,
                request.ContentType);

            var response = new BasicResponse()
            {
                Success = true,
                Data = url,
            };

            return Task.FromResult(response);
        }
    }
}
