using System;
using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Audiences
{
    public class DeleteAudienceCommand : IRequest<BasicResponse>
    {
        [Required]
        public Guid AudienceId { get; private set; }

        public DeleteAudienceCommand(Guid audienceId)
        {
            AudienceId = audienceId;
        }
    }
}
