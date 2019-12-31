using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socialite.WebAPI.Application.Commands.Identity;

namespace Socialite.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIdentityUser([FromBody] CreateIdentityUserCommand createIdentityUserCommand)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(createIdentityUserCommand);

                if (result)
                {
                    return Ok();
                }
            }
            return BadRequest(createIdentityUserCommand);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginIdentityUser([FromBody] LoginIdentityUserCommand loginIdentityUserCommand)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(loginIdentityUserCommand);

                if (result != null)
                {
                    return Ok(result);
                }
            }

            return BadRequest(loginIdentityUserCommand);
        }

    }
}