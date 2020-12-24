using System.IO;
using System.Threading.Tasks;
using LifeCMS.EventBus.Common.Interfaces;
using LifeCMS.EventBus.IntegrationEvents.Email;
using LifeCMS.Services.ContentCreation.Domain.Common;
using Scriban;

namespace LifeCMS.Services.ContentCreation.API.Services.Messaging
{
    public class EmailMessagingService : IEmailMessagingService
    {
        private readonly IEventBus _eventBus;

        public EmailMessagingService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async void DispatchEmail(
            EmailTemplate template,
            EmailAddress fromEmailAddress,
            EmailAddress toEmailAddress,
            string subject,
            object dataModel
        )
        {
            var templateFileString = await LoadTemplateAsync(template);

            var renderedTemplate = await RenderTemplateAsync(
                templateFileString,
                dataModel);

            var @event = CreateIntegrationEvent(
                fromEmailAddress,
                toEmailAddress,
                subject,
                renderedTemplate);

            _eventBus.Publish(@event);
        }

        private SendEmailEvent CreateIntegrationEvent(
            EmailAddress fromEmailAddress,
            EmailAddress toEmailAddress,
            string subject,
            string body)
        {
            return new SendEmailEvent()
            {
                From = fromEmailAddress.Value,
                To = new[] { toEmailAddress.Value },
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
        }

        private ValueTask<string> RenderTemplateAsync(
            string templateMarkup,
            object dataModel)
        {
            return Template
                .ParseLiquid(templateMarkup)
                .RenderAsync(dataModel);
        }

        private Task<string> LoadTemplateAsync(EmailTemplate template)
        {
            var fileName = template switch
            {
                EmailTemplate.Hero => "HeroTemplate",
                _ => "HeroTemplate"
            };

            var templateFilePath = $"Services/Messaging/EmailTemplates/{fileName}.html.liquid";

            return File.OpenText(templateFilePath).ReadToEndAsync();
        }
    }
}
