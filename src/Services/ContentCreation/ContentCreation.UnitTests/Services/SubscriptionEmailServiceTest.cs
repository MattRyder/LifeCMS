using System;
using LifeCMS.Services.ContentCreation.API.Services.Audiences;
using LifeCMS.Services.ContentCreation.API.Services.Messaging;
using LifeCMS.Services.ContentCreation.Domain.Common;
using Moq;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Services
{
    public class SubscriptionEmailServiceTest
    {
        private readonly Mock<IEmailMessagingService> _emailMessagingServiceMock;

        public SubscriptionEmailServiceTest()
        {
            _emailMessagingServiceMock = new Mock<IEmailMessagingService>();
        }

        [Fact]
        public void Constructor_ThrowsException_GivenNullEmailMessagingService()
        {
            var fromEmailAddress = new EmailAddress("fromEmailAddress@example.com");

            var host = "https://example.com";

            Assert.Throws<SubscriberEmailServiceException>(() =>
                new SubscriberEmailService(null, fromEmailAddress, host));
        }

        [Fact]
        public void Constructor_ThrowsException_GivenNullFromEmail()
        {
            var host = "https://example.com";

            Assert.Throws<SubscriberEmailServiceException>(() =>
                new SubscriberEmailService(_emailMessagingServiceMock.Object, null, host));
        }


        [Fact]
        public void Constructor_ThrowsException_GivenNullHost()
        {
            var fromEmailAddress = new EmailAddress("fromEmailAddress@example.com");

            Assert.Throws<SubscriberEmailServiceException>(() =>
                new SubscriberEmailService(_emailMessagingServiceMock.Object, fromEmailAddress, null));
        }
        [Fact]
        public void SendEmail_ShouldDispatchEmail_GivenValidArguments()
        {
            var audienceId = Guid.NewGuid();

            var emailAddress = new EmailAddress(
                "SendEmail_ShouldDispatchEmail_GivenValidArguments@example.com"
            );

            var name = "SubscriptionEmailServiceTestUser";

            var token = "SubscriptionEmailServiceTestToken";

            var fromEmailAddress = new EmailAddress(
                "SendEmail_ShouldDispatchEmail_GivenValidArguments@example.com"
            );

            _emailMessagingServiceMock
                .Setup(x => x.DispatchEmail(
                    EmailTemplate.Hero,
                    It.IsAny<EmailAddress>(),
                    It.IsAny<EmailAddress>(),
                    It.IsAny<string>(),
                    It.IsAny<SubscriberEmailViewModel>()
                )).Verifiable();

            var service = new SubscriberEmailService(
                _emailMessagingServiceMock.Object,
                fromEmailAddress,
                "https://example.com");

            service.SendEmail(
                audienceId,
                emailAddress,
                name,
                token
            );

            _emailMessagingServiceMock.Verify(x =>
                x.DispatchEmail(
                    EmailTemplate.Hero,
                    fromEmailAddress,
                    emailAddress,
                    It.IsAny<string>(),
                    It.IsAny<SubscriberEmailViewModel>())
            );
        }
    }
}
