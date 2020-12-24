using System;
using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.API.Services.Messaging;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.API.Services.Audiences
{
    public class SubscriberEmailServiceException : Exception
    {
        public SubscriberEmailServiceException(string message) : base(message)
        {
        }
    }

    public class SubscriberEmailService : ISubscriberEmailService
    {
        private readonly IEmailMessagingService _emailMessagingService;

        private readonly EmailAddress _fromEmailAddress;

        private readonly string _host;

        public SubscriberEmailService(
            IEmailMessagingService emailMessagingService,
            EmailAddress fromEmailAddress,
            string host)
        {
            _emailMessagingService = emailMessagingService ??
                throw new SubscriberEmailServiceException(nameof(emailMessagingService));

            _fromEmailAddress = fromEmailAddress ??
                throw new SubscriberEmailServiceException(nameof(fromEmailAddress));

            _host = host ??
                throw new SubscriberEmailServiceException(nameof(host));
        }

        public void SendEmail(
            Guid audienceId,
            EmailAddress emailAddress,
            string name,
            string subscriberToken)
        {
            var viewModel = GetSubscriberEmailViewModel(audienceId, name, subscriberToken);

            _emailMessagingService.DispatchEmail(
                EmailTemplate.Hero,
                _fromEmailAddress,
                emailAddress,
                "You have been invited to subscribe to a newsletter.",
                viewModel
            );
        }

        private string GetCallToActionLink(
            Guid audienceId,
            string subscriberToken)
        {
            return $"{_host}/subscribers/confirm?audienceId={audienceId}&subscriberToken={subscriberToken}";
        }

        private SubscriberEmailViewModel GetSubscriberEmailViewModel(
            Guid audienceId,
            string name,
            string subscriberToken)
        {
            return new SubscriberEmailViewModel()
            {
                Title = !string.IsNullOrWhiteSpace(name) ? $"Welcome, {name}!" : "Welcome!",
                LeadText = "You have been invited to subscribe to a newsletter.",
                HeroImage = "https://media.giphy.com/media/26xBukhJ0i8KXADYc/giphy.gif",
                CallToAction = new CallToAction()
                {
                    Link = GetCallToActionLink(audienceId, subscriberToken),
                    Text = "Confirm your subscription »"
                },
                Paragraphs = new List<Paragraph>()
                {
                    new Paragraph()
                    {
                        Title = "Your consent is important.",
                        Text = @"
                        Before we can start your subscription, we'll need to 
                        confirm that you want to opt-in to recieve this newsletter.
                        If you don't confirm your subscription using the button
                        above, you won't recieve any more emails from this newsletter."
                    }
                }
            };
        }
    }
}
