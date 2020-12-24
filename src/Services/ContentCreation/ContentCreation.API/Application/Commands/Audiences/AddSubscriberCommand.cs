using System;
using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Audiences
{
    public class AddSubscriberCommand : IRequest<BasicResponse>
    {
        [Required]
        public Guid AudienceId { get; private set; }

        [Required]
        public EmailAddress EmailAddress { get; private set; }

        public string Name { get; private set; }

        public bool ConsentConfirmed { get; private set; }

        public AddSubscriberCommand(
            Guid audienceId,
            string name,
            string emailAddress,
            bool consentConfirmed = false
        )
        {
            AudienceId = audienceId;

            Name = name;

            EmailAddress = new EmailAddress(emailAddress);

            ConsentConfirmed = consentConfirmed;
        }
    }
}
