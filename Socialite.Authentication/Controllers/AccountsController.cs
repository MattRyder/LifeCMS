using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Socialite.Authentication.Application.Commands.Identity;

namespace Socialite.Authentication.Controllers
{
    // [ApiController]
    [EnableCors("CorsPolicy")]
    [Route("api/v1/[controller]")]
    public class AccountsController : Controller
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
        public async Task<IActionResult> Login([FromBody] LoginIdentityUserCommand loginIdentityUserCommand)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(loginIdentityUserCommand);

                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest(ModelState.ValidationState);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromForm] LogoutIdentityUserCommand logoutIdentityUserCommand)
        {
            var result = await _mediator.Send(logoutIdentityUserCommand);

            if (result.Success)
            {
                var returnUrl = result.Data.PostLogoutRedirectUri;

                return Ok();
            }

            return BadRequest(result.Errors);
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