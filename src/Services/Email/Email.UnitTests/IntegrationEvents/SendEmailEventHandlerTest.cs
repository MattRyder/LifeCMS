using LifeCMS.Services.Email.API.IntegrationEvents;
using LifeCMS.Services.Email.Domain.Concrete;
using LifeCMS.Services.Email.Infrastructure.Interfaces;
using LifeCMS.Services.Email.Infrastructure.Smtp;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LifeCMS.Services.Email.UnitTests.IntegrationEvents
{
    public class SendEmailEventHandlerTest
    {
        private readonly Mock<IEmailClient> _emailClientMock;

        private readonly Mock<ILogger<SendEmailEventHandler>> _loggerMock;

        public SendEmailEventHandlerTest()
        {
            _emailClientMock = new Mock<IEmailClient>();

            _loggerMock = new Mock<ILogger<SendEmailEventHandler>>();
        }

        [Fact]
        public void Handle_ReturnsTrue_GivenValidEvent()
        {
            var handler = new SendEmailEventHandler(
                _emailClientMock.Object,
                _loggerMock.Object
            );

            var sendEmailEvent = new SendEmailEvent()
            {
                To = new[] { "test@example.com" },
                Subject = "Email Subject",
                Body = "Email Body"
            };

            var result = handler.Handle(sendEmailEvent);

            Assert.True(result);
        }

        [Fact]
        public void Handle_ReturnsFalse_WhenEmailClientThrows()
        {
            var sendEmailEvent = new SendEmailEvent()
            {
                To = new[] { "test@example.com" },
                Subject = "Email Subject",
                Body = "Email Body"
            };

            _emailClientMock
                .Setup(x => x.Send(It.IsAny<EmailMessage>()))
                .Throws(new EmailClientException(""));

            var handler = new SendEmailEventHandler(
                _emailClientMock.Object,
                _loggerMock.Object
            );

            var result = handler.Handle(sendEmailEvent);

            Assert.False(result);
        }
    }
}