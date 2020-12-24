using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Audiences;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Audiences;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Filters;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LifeCMS.Services.ContentCreation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AudiencesController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IAudienceQueries _audienceQueries;

        private readonly IUserAccessor _userAccessor;

        public AudiencesController(
            IMediator mediator,
            IAudienceQueries audienceQueries,
            IUserAccessor userAccessor)
        {
            _mediator = mediator;

            _userAccessor = userAccessor;

            _audienceQueries = audienceQueries;
        }

        // GET: api/audiences
        [HttpGet]
        public async Task<IActionResult> GetAudiences()
        {
            var audiences = await _audienceQueries.FindAudiences(_userAccessor.Id);

            return Ok(audiences);
        }

        // GET: api/audiences/{audienceId:guid}/subscribers
        [HttpGet("{audienceId:guid}/subscribers")]
        public async Task<IActionResult> GetAudienceSubscribers(Guid audienceId)
        {
            var audiences = await _audienceQueries.FindAudienceSubscribers(audienceId);

            return Ok(audiences);
        }

        // POST: api/audiences
        [HttpPost]
        [Authorize]
        [ModelStateValidation]
        public async Task<IActionResult> CreateAudience(CreateAudienceCommand command)
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

        // POST: api/audiences/addSubscriber
        [HttpPost("addSubscriber")]
        [Authorize]
        [ModelStateValidation]
        public async Task<IActionResult> AddSubscriber(AddSubscriberCommand command)
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

        [HttpPost("{audienceId:guid}/updateName")]
        [Authorize]
        public async Task<IActionResult> UpdateName(
            [FromRoute] Guid audienceId,
            [FromBody] UpdateAudienceNameRequestBody body
        )
        {
            var command = new UpdateAudienceNameCommand(audienceId, body.Name);

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

        // DELETE: api/audiences/{audienceId:guid}
        [HttpDelete("{audienceId:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteAudience([FromRoute] Guid audienceId)
        {
            var command = new DeleteAudienceCommand(audienceId);

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
