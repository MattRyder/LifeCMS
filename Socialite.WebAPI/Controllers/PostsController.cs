using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.WebAPI.Application.Commands.Posts;
using Socialite.WebAPI.Application.Enums;

namespace Socialite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IPostRepository _postRepository;

        public PostsController(IMediator mediator, IPostRepository postRepository)
        {
            _mediator = mediator;
            _postRepository = postRepository;
        }

        // GET: api/Posts
        [HttpGet(Name = "GetPosts")]
        public async Task<IActionResult> Get()
        {
            // var statuses = await _statusQueries.FindAllAsync();
            return Ok();
        }

        // GET: api/Posts/5
        [HttpGet("{id}", Name = "GetPost")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                // var status = await _statusQueries.FindStatus(id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
            var command = new CreatePostCommand(post);

            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest(post);
            }
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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
                    return BadRequest();
                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
