using System;
using System.ComponentModel.DataAnnotations;
using LifeCMS.Services.ContentCreation.API.Services.Newsletters.HtmlGeneration;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Newsletters
{
    public class UpdateNewsletterBodyCommand : IRequest<bool>
    {
        [Required]
        public Guid Id { get; private set; }

        [Required]
        public NewsletterBody Body { get; private set; }

        public UpdateNewsletterBodyCommand(Guid id, string body)
        {
            Id = id;

            Body = CreateNewsletterBody(body);
        }

        private NewsletterBody CreateNewsletterBody(string body)
        {
            var html = new HtmlGenerationService(body);

            return new NewsletterBody(body, html.GenerateHtml());
        }
    }
}
