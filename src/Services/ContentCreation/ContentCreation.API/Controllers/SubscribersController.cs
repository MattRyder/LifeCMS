using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Audiences;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LifeCMS.Services.ContentCreation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SubscribersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscribersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/subscribers/confirm
        [HttpPost("confirm")]
        [ModelStateValidation]
        public async Task<IActionResult> ConfirmSubscriber(
            [FromBody] ConfirmSubscriberCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
