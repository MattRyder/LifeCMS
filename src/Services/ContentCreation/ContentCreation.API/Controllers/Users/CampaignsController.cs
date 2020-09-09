using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Campaigns;
using Microsoft.AspNetCore.Mvc;

namespace LifeCMS.Services.ContentCreation.API.Controllers.Users
{
    [ApiController]
    [Route("api/users/{userId:guid}/[controller]")]
    public class CampaignsController : ControllerBase
    {
        private readonly ICampaignQueries _campaignQueries;

        public CampaignsController(ICampaignQueries campaignQueries)
        {
            _campaignQueries = campaignQueries;
        }

        // GET: api/users/{userId:guid}/campaigns
        [HttpGet]
        public async Task<IActionResult> GetCampaigns(Guid userId)
        {
            var campaigns = await _campaignQueries.FindCampaigns(userId);

            return Ok(campaigns);
        }
    }
}