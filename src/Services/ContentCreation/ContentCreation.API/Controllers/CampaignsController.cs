using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Campaigns;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Filters;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LifeCMS.Services.ContentCreation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CampaignsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/campaigns
        [HttpPost]
        public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignCommand command)
        {
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest(command);
            }
        }

        // DELETE: api/campaigns/{campaignId:guid}
        [HttpDelete]
        [ModelStateValidation]
        [Route("{campaignId:guid}")]
        public async Task<IActionResult> DeleteCampaign([FromRoute] Guid campaignId)
        {
            var command = new DeleteCampaignCommand(campaignId);

            var result = await _mediator.Send(command);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(command);
            }
        }

        // POST: api/campaigns/{campaignId:guid}/updateName
        [HttpPost]
        [ModelStateValidation]
        [Route("{campaignId:guid}/updateName")]
        public async Task<IActionResult> UpdateName(
            [FromRoute] Guid campaignId,
            [FromBody] UpdateCampaignNameDTO dto
        )
        {
            var command = new UpdateCampaignNameCommand(campaignId, dto.Name);

            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest(dto);
            }
        }

        // POST: api/campaigns/{id:guid}/updateSubject
        [HttpPost]
        [ModelStateValidation]
        [Route("{campaignId:guid}/updateSubject")]
        public async Task<IActionResult> UpdateSubject(
            [FromRoute] Guid campaignId,
            [FromBody] Subject subject
        )
        {
            var command = new UpdateCampaignSubjectCommand(campaignId, subject);

            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest(subject);
            }
        }
    }
}