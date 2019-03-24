using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Socialite.WebAPI.Application.Commands.Albums;
using Socialite.WebAPI.Application.Queries.Albums;

namespace Socialite.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAlbumQueries _albumQueries;

        public AlbumsController(IMediator mediator, IAlbumQueries albumQueries)
        {
            _mediator = mediator;
            _albumQueries = albumQueries;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlbums()
        {
            var albums = await _albumQueries.FindAllAsync();

            return Ok(albums);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(int id)
        {
            var albums = await _albumQueries.FindAsync(id);

            try
            {
                return Ok(albums);
            }
            catch(KeyNotFoundException)
            {
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbum([FromBody] CreateAlbumCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(command, default(CancellationToken));

                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(command);
        }

        [HttpPost]
        [Route("{albumId}/upload")]
        public async Task<IActionResult> UploadPhoto(int albumId, [FromForm] string name, [FromForm] string caption, IFormFile file)
        {
            var command = new UploadPhotoCommand(albumId, name, file, caption);

            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok();
            }

            return BadRequest(command);
        }
    }
}