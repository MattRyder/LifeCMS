using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socialite.Authentication.Application.Commands.Identity;

namespace Socialite.Authentication.Controllers
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

                if (result.Success)
                {
                    return Ok(result);
                }
                
                return BadRequest(result);
            }

            return BadRequest(ModelState.ValidationState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginIdentityUser([FromBody] LoginIdentityUserCommand loginIdentityUserCommand, [FromQuery] string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(loginIdentityUserCommand);

                if (result.Success)
                {
                    return LocalRedirect(returnUrl);
                }
                
                return BadRequest(result);
            }

            return BadRequest(ModelState.ValidationState);
        }

        [HttpGet("Error")]
        public IActionResult Error(string errorId)
        {
            // var message = await _interaction.GetErrorContextAsync(errorId);

            return Ok();
            // return LocalRedirect($"/accounts/error?error={message.Error}&error_description={message.ErrorDescription}");
        }
    }
}