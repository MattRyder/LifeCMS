using System;
using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Campaigns
{
    public class CreateCampaignCommand : IRequest<bool>
    {
        [Required]
        public Guid NewsletterTemplateId { get; private set; }

        [Required]
        public Guid UserProfileId { get; private set; }

        [Required]
        public string Name { get; private set; }

        [Required]
        public Subject Subject { get; private set; }

        [Required]
        public DateTime ScheduledDate { get; private set; }

        [Required]
        public bool UseSubscriberTimeZone { get; private set; }

        public CreateCampaignCommand(
            Guid newsletterTemplateId,
            Guid userProfileId,
            string name,
            Subject subject,
            DateTime scheduledDate,
            bool useSubscriberTimeZone
        )
        {
            NewsletterTemplateId = newsletterTemplateId;

            UserProfileId = userProfileId;

            Name = name ?? throw new CampaignDomainException(nameof(name));

            Subject = subject ?? throw new CampaignDomainException(nameof(subject));

            ScheduledDate = IsScheduledDateValid(scheduledDate)
                ? scheduledDate
                : throw new CampaignDomainException(nameof(scheduledDate));

            UseSubscriberTimeZone = useSubscriberTimeZone;
        }

        private static bool IsScheduledDateValid(DateTime dateTime)
        {
            return DateTime.Now < dateTime.AddHours(1);
        }
    }
}