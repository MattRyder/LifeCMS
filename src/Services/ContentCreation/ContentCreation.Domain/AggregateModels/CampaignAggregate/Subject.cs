using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate
{
    public class Subject : ValueObject
    {
        public string SubjectLineText { get; private set; }

        public string PreviewText { get; private set; }

        public Subject(string subjectLineText, string previewText)
        {
            SubjectLineText = subjectLineText ?? throw new CampaignDomainException(nameof(subjectLineText));

            PreviewText = previewText ?? throw new CampaignDomainException(nameof(previewText));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return SubjectLineText;
            yield return PreviewText;
        }
    }

}