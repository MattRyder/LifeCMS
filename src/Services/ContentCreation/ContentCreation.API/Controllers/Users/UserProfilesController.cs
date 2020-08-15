using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LifeCMS.Services.ContentCreation.API.Application.Commands.UserProfiles;
using LifeCMS.Services.ContentCreation.API.Application.Queries.UserProfiles;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Filters;

namespace LifeCMS.Services.ContentCreation.API.Controllers.Users
{
    [ApiController]
    [Route("api/users/{userId:guid}/[controller]")]
    public class UserProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserProfileQueries _userProfileQueries;

        public UserProfilesController(IMediator mediator, IUserProfileQueries userProfileQueries)
        {
            _mediator = mediator;

            _userProfileQueries = userProfileQueries;
        }

        // GET: api/users/{userId:guid}/userprofiles
        [HttpGet]
        public async Task<IActionResult> GetUserProfiles(Guid userId)
        {
            var post = await _userProfileQueries.FindUserProfiles(userId);

            return Ok(post);
        }

        // POST: api/users/{userId:guid}/userprofiles
        [HttpPost]
        [Authorize]
        [ModelStateValidation]
        public async Task<IActionResult> CreateUserProfile([FromBody] CreateUserProfileCommand command)
        {
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok();
            }

            return BadRequest(command);
        }

        // DELETE api/users/{userId:guid}/userprofiles/{id:guid}
        [Authorize]
        [ModelStateValidation]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUserProfile([FromRoute] Guid id)
        {
            var command = new DeleteUserProfileCommand(id);

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
