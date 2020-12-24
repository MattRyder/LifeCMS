using System;
using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Audiences
{
    public class UpdateAudienceNameRequestBody
    {
        public string Name { get; set; }
    }

    public class UpdateAudienceNameCommand : IRequest<BasicResponse>
    {
        [Required]
        public Guid AudienceId { get; private set; }

        [Required]
        public string Name { get; private set; }

        public UpdateAudienceNameCommand(
            Guid audienceId,
            string name
        )
        {
            AudienceId = audienceId;

            Name = name;
        }
    }
}
