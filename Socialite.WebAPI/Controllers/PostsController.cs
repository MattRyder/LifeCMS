using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.WebAPI.Application.Commands.Posts;
using Socialite.WebAPI.Application.Enums;
using Socialite.WebAPI.Queries.Posts;

namespace Socialite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IPostQueries _postQueries;

        public PostsController(IMediator mediator, IPostQueries postQueries)
        {
            _mediator = mediator;
            _postQueries = postQueries;
        }

        // GET: api/Posts
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postQueries.FindAllAsync();

            return Ok(posts);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            try
            {
                var post = await _postQueries.FindAsync(id);

                return Ok(post);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
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

        [HttpPut("{id}/publish")]
        public async Task<IActionResult> PublishPost(int id)
        {
            var command = new PublishPostCommand(id);

            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok();
            }

            return BadRequest(command);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var command = new DeletePostCommand(id);

            var result = await _mediator.Send(command);

            switch (result)
            {
                case DeleteCommandResult.Success:
                    return Ok();
                case DeleteCommandResult.NotFound:
                    return NotFound();
                case DeleteCommandResult.Failure:
                default:
                    return BadRequest();
            }
        }
    }
}
