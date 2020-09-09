using System;
using Bogus;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;

namespace LifeCMS.Services.ContentCreation.UnitTests.Factories
{
    public class CampaignFactory : FactoryBase<Campaign>
    {
        public static Campaign Create(
            Guid userId,
            Guid newsletterTemplateId,
            Guid userProfileId
        )
        {
            var name = "Campaign Test Name";

            var subject = CreateSubject();

            var scheduledDate = DateTime.UtcNow.AddHours(1);

            return new Faker<Campaign>().CustomInstantiator(f =>
            {
                return new Campaign(
                    userId,
                    newsletterTemplateId,
                    userProfileId,
                    name,
                    subject,
                    scheduledDate,
                    false
                );
            });

        }

        private static Subject CreateSubject()
        {
            return new Faker<Subject>().CustomInstantiator(f =>
            {
                return new Subject(
                    subjectLineText: f.Lorem.Sentence(7),
                    previewText: f.Lorem.Sentence(7)
                );
            });
        }
    }
}