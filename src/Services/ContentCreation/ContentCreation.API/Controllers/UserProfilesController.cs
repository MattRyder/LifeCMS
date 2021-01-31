using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LifeCMS.Services.ContentCreation.API.Application.Commands.UserProfiles;
using LifeCMS.Services.ContentCreation.API.Application.Queries.UserProfiles;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Filters;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;

namespace LifeCMS.Services.ContentCreation.API.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IUserProfileQueries _userProfileQueries;

        private readonly IUserAccessor _userAccessor;

        public UserProfilesController(
            IMediator mediator,
            IUserProfileQueries userProfileQueries,
            IUserAccessor userAccessor)
        {
            _mediator = mediator;

            _userProfileQueries = userProfileQueries;

            _userAccessor = userAccessor;
        }

        // GET: api/userprofiles
        [HttpGet]
        public async Task<IActionResult> GetUserProfiles()
        {
            var userProfiles = await _userProfileQueries.FindUserProfiles(
                _userAccessor.Id);

            return Ok(userProfiles);
        }

        // POST: api/userprofiles
        [HttpPost]
        [ModelStateValidation]
        public async Task<IActionResult> CreateUserProfile(
            [FromBody] CreateUserProfileCommand command)
        {
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok();
            }

            return BadRequest(command);
        }

        // DELETE api/userprofiles/{id:guid}
        [ModelStateValidation]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUserProfile([FromRoute] Guid id)
        {
            var command = new DeleteUserProfileCommand(id);

            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result);
            }
        }

        // PUT api/userprofiles
        [HttpPut]
        [ModelStateValidation]
        public async Task<IActionResult> UpdateUserProfile(
            UpdateUserProfileCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
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
