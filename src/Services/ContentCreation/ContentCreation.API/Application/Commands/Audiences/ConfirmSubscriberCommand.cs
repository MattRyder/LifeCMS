using System;
using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Audiences
{
    public class ConfirmSubscriberCommand : IRequest<BasicResponse>
    {
        [Required]
        public Guid AudienceId { get; private set; }

        [Required]
        public string SubscriberToken { get; private set; }

        public ConfirmSubscriberCommand(
            Guid audienceId,
            string subscriberToken)
        {
            AudienceId = audienceId;

            SubscriberToken = subscriberToken;
        }
    }
}
