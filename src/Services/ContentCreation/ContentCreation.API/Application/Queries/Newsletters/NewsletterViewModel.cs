using System;
using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Newsletters
{
    public class NewsletterViewModel : ValueObject
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string DesignSource { get; set; }

        public string Html { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return UserId;
            yield return Name;
            yield return DesignSource;
            yield return Html;
            yield return CreatedAt;
            yield return UpdatedAt;
        }

        public static NewsletterViewModel FromModel(Newsletter newsletter)
        {
            return new NewsletterViewModel
            {
                Id = newsletter.Id,
                UserId = newsletter.UserId,
                Name = newsletter.Name,
                DesignSource = newsletter.Body.DesignSource,
                Html = newsletter.Body.Html,
                CreatedAt = newsletter.CreatedAt,
                UpdatedAt = newsletter.UpdatedAt
            };
        }
    }
}
