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
        public readonly IPostRepository _postRepository;
        public readonly IPostQueries _postQueries;

        public PostsController(IMediator mediator, IPostRepository postRepository, IPostQueries postQueries)
        {
            _mediator = mediator;
            _postRepository = postRepository;
            _postQueries = postQueries;
        }

        // GET: api/Posts
        [HttpGet(Name = "GetPosts")]
        public async Task<IActionResult> Get()
        {
            var posts = await _postQueries.FindAllAsync();

            return Ok(posts);
        }

        // GET: api/Posts/5
        [HttpGet("{id}", Name = "GetPost")]
        public async Task<IActionResult> Get(int id)
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
                default:
                    return BadRequest();
            }
        }
    }
}
