using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socialite.Domain.AggregateModels.UsersAggregate;
using Socialite.WebAPI.Application.Commands.Users;
using Socialite.WebAPI.Application.Queries.Users;

namespace Socialite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserQueries _userQueries;

        public UsersController(IMediator mediator, IUserQueries userQueries)
        {
            _mediator = mediator;
            _userQueries = userQueries;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            UserViewModel result;

            try
            {
                result = await _userQueries.FindAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return (result != null) ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(command);

                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(command);
        }
    }
}