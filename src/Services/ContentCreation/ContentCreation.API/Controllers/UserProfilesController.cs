using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LifeCMS.Services.ContentCreation.API.Application.Commands.UserProfiles;
using LifeCMS.Services.ContentCreation.API.Application.Queries.UserProfiles;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Filters;

namespace LifeCMS.Services.ContentCreation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserProfileQueries _userProfileQueries;

        public UserProfilesController(IMediator mediator, IUserProfileQueries userProfileQueries)
        {
            _mediator = mediator;

            _userProfileQueries = userProfileQueries;
        }

        // GET: api/UserProfiles/5
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserProfile(Guid userId)
        {
            try
            {
                var post = await _userProfileQueries.FindUserProfile(userId);

                return Ok(post);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/UserProfiles
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
    }
}
