using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Campaigns;

namespace LifeCMS.Services.ContentCreation.IntegrationTests.Comparers
{
    public class CampaignViewModelComparer : IEqualityComparer<CampaignViewModel>
    {
        public bool Equals([AllowNull] CampaignViewModel x, [AllowNull] CampaignViewModel y)
        {
            return x.Id.Equals(y.Id)
                && x.UserId.Equals(y.UserId)
                && x.NewsletterTemplateId.Equals(y.NewsletterTemplateId)
                && x.UserProfileId.Equals(y.UserProfileId)
                && x.Name.Equals(y.Name)
                && x.SubjectLineText.Equals(y.SubjectLineText)
                && x.PreviewText.Equals(y.PreviewText)
                && x.UseSubscriberTimeZone.Equals(y.UseSubscriberTimeZone)
                && DateCompare(x.ScheduledDate, y.ScheduledDate)
                && DateCompare(x.CreatedAt, y.CreatedAt)
                && DateCompare(x.UpdatedAt, y.UpdatedAt);
        }

        public int GetHashCode([DisallowNull] CampaignViewModel obj)
        {
            return obj.Id.GetHashCode()
                ^ obj.UserId.GetHashCode()
                ^ obj.NewsletterTemplateId.GetHashCode()
                ^ obj.UserProfileId.GetHashCode()
                ^ obj.Name.GetHashCode()
                ^ obj.SubjectLineText.GetHashCode()
                ^ obj.PreviewText.GetHashCode()
                ^ obj.ScheduledDate.GetHashCode()
                ^ obj.UseSubscriberTimeZone.GetHashCode()
                ^ obj.CreatedAt.GetHashCode()
                ^ obj.UpdatedAt.GetHashCode();
        }

        private bool DateCompare([DisallowNull] DateTime x, [DisallowNull] DateTime y)
        {
            return Math.Abs((x - y).TotalSeconds) < 1;
        }
    }
}