using System.Threading.Tasks;
using LifeCMS.EventBus.Common.Interfaces;
using LifeCMS.EventBus.IntegrationEvents.Email;
using LifeCMS.Services.ContentCreation.Domain.Common;
using Microsoft.Extensions.FileProviders;
using Scriban;

namespace LifeCMS.Services.ContentCreation.API.Services.Messaging
{
    public class EmailMessagingService : IEmailMessagingService
    {
        private readonly IFileProvider _fileProvider;

        private readonly IEventBus _eventBus;

        public EmailMessagingService(
            IFileProvider fileProvider,
            IEventBus eventBus
        )
        {
            _fileProvider = fileProvider;

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

            var templateFilePath =
                $"Services/Messaging/EmailTemplates/{fileName}.html.liquid";

            return GetTemplateResourceAsync(templateFilePath);
        }

        private async Task<string> GetTemplateResourceAsync(string path)
        {
            var file = _fileProvider.GetFileInfo(path).CreateReadStream();

            var buffer = new byte[file.Length];

            await file.ReadAsync(buffer, 0, (int)file.Length);

            return System.Text.Encoding.UTF8.GetString(buffer);
        }
    }
}
