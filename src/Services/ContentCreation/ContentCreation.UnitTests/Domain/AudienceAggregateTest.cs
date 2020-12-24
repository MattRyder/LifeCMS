using System;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Domain
{
    public class AudienceAggregateTest
    {
        [Fact]
        public void Constructor_CreatesAudience_GivenValidParameters()
        {
            var audience = AudienceFactory.Create(Guid.NewGuid());

            Assert.NotNull(audience);

            Assert.NotNull(audience.Subscribers);
        }

        [Fact]
        public void Constructor_ThrowsException_GivenNullName()
        {
            Assert.Throws<AudienceDomainException>(
                () => new Audience(Guid.NewGuid(), null)
            );
        }

        [Fact]
        public void AddSubscriber_ShouldCreateSubscriber_GivenValidEmailAddress()
        {
            var audience = AudienceFactory.Create(Guid.NewGuid());

            var name = "J Smith";

            var emailAddress = new EmailAddress(
                "AddSubscriber_ShouldCreateSubscriber_GivenValidEmailAddress@example.com"
            );

            var subscriberToken = "AABBCCDDEEFF";

            audience.AddSubscriber(name, emailAddress, subscriberToken);

            Assert.Equal(1, audience.Subscribers.Count);

            Assert.Equal(1, audience.Events.Count);
        }

        [Fact]
        public void UpdateName_ShouldUpdateName_GivenValidName()
        {
            var audience = AudienceFactory.Create(Guid.NewGuid());

            var newName = "Robert California";

            audience.UpdateName(newName);

            Assert.Equal(newName, audience.Name);
        }

        [Fact]
        public void UpdateName_ThrowsException_GivenNullName()
        {
            var audience = AudienceFactory.Create(Guid.NewGuid());

            Assert.Throws<AudienceDomainException>(() => audience.UpdateName(null));
        }

        [Fact]
        public void UpdateName_ThrowsException_GivenEmptyName()
        {
            var audience = AudienceFactory.Create(Guid.NewGuid());

            Assert.Throws<AudienceDomainException>(() => audience.UpdateName(""));
        }
    }
}
