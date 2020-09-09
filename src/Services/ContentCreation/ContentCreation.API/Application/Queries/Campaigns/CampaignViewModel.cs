using System;
using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Campaigns
{
    public class CampaignViewModel : ValueObject
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid NewsletterTemplateId { get; set; }

        public Guid UserProfileId { get; set; }

        public string Name { get; set; }

        public string SubjectLineText { get; set; }

        public string PreviewText { get; set; }

        public DateTime ScheduledDate { get; set; }

        public bool UseSubscriberTimeZone { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public static CampaignViewModel FromModel(Campaign campaign)
        {
            return new CampaignViewModel
            {
                Id = campaign.Id,
                UserId = campaign.UserId,
                NewsletterTemplateId = campaign.NewsletterTemplateId,
                UserProfileId = campaign.UserProfileId,
                Name = campaign.Name,
                SubjectLineText = campaign.Subject.SubjectLineText,
                PreviewText = campaign.Subject.PreviewText,
                ScheduledDate = campaign.ScheduledDate,
                UseSubscriberTimeZone = campaign.UseSubscriberTimeZone,
                CreatedAt = campaign.CreatedAt,
                UpdatedAt = campaign.UpdatedAt
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return UserId;
            yield return NewsletterTemplateId;
            yield return UserProfileId;
            yield return Name;
            yield return SubjectLineText;
            yield return PreviewText;
            yield return ScheduledDate;
            yield return UseSubscriberTimeZone;
            yield return CreatedAt;
            yield return UpdatedAt;
        }
    }
}