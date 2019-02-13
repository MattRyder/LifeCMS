using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.WebAPI.Application.Commands.Status;
using Socialite.WebAPI.Queries.Status;

namespace Socialite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IStatusRepository _statusRepository;
        public readonly IStatusQueries _statusQueries;

        public StatusesController(IMediator mediator, IStatusRepository statusRepository, IStatusQueries statusQueries)
        {
            _mediator = mediator;
            _statusRepository = statusRepository;
            _statusQueries = statusQueries;
        }

        // GET: api/Statuses
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var statuses = await _statusQueries.FindAllAsync();
            return Ok(statuses);
        }

        // GET: api/Statuses/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
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
        public async Task<IActionResult> Post([FromBody] Status status)
        {
            var command = new CreateStatusCommand(status);

            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest(status);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteStatusCommand(id);
            var result = await _mediator.Send(command);

            switch (result)
            {
                case DeleteStatusCommandResult.Success:
                    return Ok();
                case DeleteStatusCommandResult.NotFound:
                    return NotFound();
                case DeleteStatusCommandResult.Failure:
                    return BadRequest();
                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
