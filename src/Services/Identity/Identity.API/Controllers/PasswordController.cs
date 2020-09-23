using System.Threading.Tasks;
using LifeCMS.Services.Identity.API.Application.Commands.Password;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LifeCMS.Services.Identity.API.Controllers
{
    [Route("/api/v1/[controller]")]
    public class PasswordController : Controller
    {
        private readonly IMediator _mediator;

        public PasswordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("RequestReset")]
        public async Task<IActionResult> RequestReset(
            [FromBody] RequestPasswordResetCommand command
        )
        {
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest(command);
            }
        }

        [HttpPost("Reset")]
        public async Task<IActionResult> Reset(
            [FromBody] ConfirmPasswordResetCommand command
        )
        {
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest(command);
            }
        }
    }
}
