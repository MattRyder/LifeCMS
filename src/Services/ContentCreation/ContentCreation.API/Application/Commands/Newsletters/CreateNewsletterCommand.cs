using System;
using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.ContentCreation.API.Services.Newsletters.HtmlGeneration;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Newsletters
{
    public class CreateNewsletterCommand : IRequest<bool>
    {
        [Required]
        public Guid UserId { get; private set; }

        [Required]
        public string Name { get; private set; }

        [Required]
        public NewsletterBody Body { get; private set; }

        public CreateNewsletterCommand(Guid userId, string name, string body)
        {
            UserId = userId;

            Name = name;

            Body = CreateNewsletterBody(body);
        }

        private NewsletterBody CreateNewsletterBody(string body)
        {
            var html = new HtmlGenerationService(body);

            return new NewsletterBody(body, html.GenerateHtml());
        }
    }
}
