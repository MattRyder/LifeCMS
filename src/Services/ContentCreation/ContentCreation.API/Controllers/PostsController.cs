using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Posts;
using LifeCMS.Services.ContentCreation.API.Application.Enums;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Posts;

namespace LifeCMS.Services.ContentCreation.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPostQueries _postQueries;

        public PostsController(IMediator mediator, IPostQueries postQueries)
        {
            _mediator = mediator;
            _postQueries = postQueries;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postQueries.FindAllAsync();

            return Ok(posts);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(Guid id)
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
        public async Task<IActionResult> PublishPost(Guid id)
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
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var command = new DeletePostCommand(id);

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
