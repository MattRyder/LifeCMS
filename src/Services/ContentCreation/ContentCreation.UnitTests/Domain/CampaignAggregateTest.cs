using System;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Domain
{
    public class CampaignAggregateTest
    {
        [Fact]
        public void Constructor_ReturnsCampaign_GivenValidParams()
        {
            var result = CampaignFactory.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            );

            Assert.NotNull(result);
        }

        [Fact]
        public void Constructor_ThrowsException_GivenNullSubject()
        {
            static Campaign CreationFunc() => new Campaign(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                "campaign name",
                null,
                DateTime.Now,
                false
            );

            Assert.Throws<CampaignDomainException>(CreationFunc);
        }

        [Fact]
        public void UpdateName_AltersName_GivenValidName()
        {
            var campaign = CampaignFactory.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            );

            var expected = "Campaign Name 2";

            campaign.UpdateName(expected);

            var actual = campaign.Name;

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void UpdateName_ThrowsException_GivenNullName()
        {
            var campaign = CampaignFactory.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            );

            Assert.Throws<CampaignDomainException>(() =>
            {
                campaign.UpdateName(null);
            });
        }


        [Fact]
        public void UpdateScheduledDate_AltersSubject_GivenValidSubject()
        {
            var campaign = CampaignFactory.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            );

            var subject = new Subject("test", "test");

            campaign.UpdateSubject(subject);

            Assert.Equal(subject, campaign.Subject);
        }

        [Fact]
        public void UpdateSubject_ThrowsException_GivenNullSubject()
        {
            var campaign = CampaignFactory.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            );

            Assert.Throws<CampaignDomainException>(() =>
            {
                campaign.UpdateSubject(null);
            });
        }

        [Fact]
        public void UpdateScheduledDate_AltersScheduledDate_GivenValidDateTime()
        {
            var campaign = CampaignFactory.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            );

            var dateTime = DateTime.Now;

            campaign.UpdateScheduledDate(dateTime);

            Assert.Equal(dateTime, campaign.ScheduledDate);
        }
    }
}