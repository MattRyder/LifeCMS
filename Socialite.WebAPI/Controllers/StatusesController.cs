using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Socialite.WebAPI.Application.Commands.Statuses;
using Socialite.WebAPI.Application.Enums;
using Socialite.WebAPI.Queries.Statuses;

namespace Socialite.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StatusesController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IStatusQueries _statusQueries;

        public StatusesController(IMediator mediator, IStatusQueries statusQueries)
        {
            _mediator = mediator;

            _statusQueries = statusQueries;
        }

        // GET: api/Statuses
        [HttpGet(Name = "GetStatuses")]
        public async Task<IActionResult> Get()
        {
            var statuses = await _statusQueries.FindAllAsync();
            return Ok(statuses);
        }

        // GET: api/Statuses/5
        [HttpGet("{id}", Name = "GetStatus")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var status = await _statusQueries.FindStatus(id);
                return Ok(status);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: api/Statuses
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateStatusCommand createStatusCommand)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(createStatusCommand);

                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(createStatusCommand);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteStatusCommand(id);

            var result = await _mediator.Send(command);

            return result switch
            {
                DeleteCommandResult.Success => Ok(),
                DeleteCommandResult.NotFound => NotFound(),
                _ => BadRequest(),
            };
        }
    }
}
