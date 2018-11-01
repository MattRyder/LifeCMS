using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Socialite.Domain.Entities;
using Socialite.Domain.Interfaces;
using Socialite.Infrastructure.DTO;

namespace Socialite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        public readonly IRepo _repository;

        public StatusesController(IRepo repository)
        {
            _repository = repository;
        }

        // GET: api/Statuses
        [HttpGet]
        public IActionResult Get()
        {
            var statuses = _repository.FindAll<Status>().Select(StatusDTO.FromModel);
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
