using Microsoft.AspNetCore.Mvc;
using Socialite.Domain.AggregateModels.StatusAggregate;

namespace Socialite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        public readonly IStatusRepository _statusRepository;

        public StatusesController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        // GET: api/Statuses
        [HttpGet]
        public IActionResult Get()
        {
            var statuses = _statusRepository.FindAll();
            return Ok(statuses);
        }

        // GET: api/Statuses/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Statuses
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Statuses/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
