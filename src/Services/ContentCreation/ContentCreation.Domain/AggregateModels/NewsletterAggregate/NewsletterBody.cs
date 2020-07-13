using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate
{
    public class NewsletterBody : ValueObject
    {
        public string DesignSource { get; private set; }

        public string Html { get; private set; }

        public NewsletterBody(string designSource, string html)
        {
            DesignSource = designSource ?? throw new NewsletterDomainException(nameof(designSource));

            Html = html ?? throw new NewsletterDomainException(nameof(html));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return DesignSource;
            yield return Html;
        }
    }
}