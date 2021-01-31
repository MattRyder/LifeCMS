using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Files;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LifeCMS.Services.ContentCreation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/files/{fileUrn}
        [HttpPost("{fileUrn}")]
        public async Task<IActionResult> GetFileUrl(string fileUrn)
        {
            var command = new CreateFileUrlCommand(fileUrn);

            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // POST: api/files
        [HttpPost]
        public async Task<IActionResult> CreatePresignedPostUrl(
            CreatePresignedPostUrlCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
