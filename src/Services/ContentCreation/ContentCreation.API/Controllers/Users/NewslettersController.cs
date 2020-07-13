using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Newsletters;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Newsletters;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LifeCMS.Services.ContentCreation.API.Controllers.Users
{
    [ApiController]
    [Route("api/users/{userId:guid}/[controller]")]
    public class NewslettersController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly INewsletterQueries _newsletterQueries;

        public NewslettersController(
            IMediator mediator,
            INewsletterQueries newsletterQueries
        )
        {
            _mediator = mediator;

            _newsletterQueries = newsletterQueries;
        }

        // GET: api/users/{userId:guid}/newsletters
        [HttpGet]
        public async Task<IActionResult> GetNewsletters(Guid userId)
        {
            try
            {
                var newsletters = await _newsletterQueries.FindNewsletters(userId);

                return Ok(newsletters);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/users/{userId:guid}/newsletters
        [HttpPost]
        [Authorize]
        [ModelStateValidation]
        public async Task<IActionResult> CreateNewsletter([FromBody] CreateNewsletterCommand command)
        {
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok();
            }

            return BadRequest(command);
        }

        // DELETE api/users/{userId:guid}/newsletters/{id:guid}
        [Authorize]
        [ModelStateValidation]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteNewsletter([FromRoute] Guid id)
        {
            var command = new DeleteNewsletterCommand(id);

            var result = await _mediator.Send(command);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(command);
            }
        }
    }
}
