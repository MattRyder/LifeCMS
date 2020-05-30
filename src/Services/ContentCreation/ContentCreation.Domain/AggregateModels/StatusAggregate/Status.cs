using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Domain.Exceptions;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.StatusAggregate
{
    public class Status : BaseEntity, IAggregateRoot
    {
        public string Mood { get; private set; }

        public string Text { get; private set; }

        public Status(string mood, string text)
        {
            Mood = !string.IsNullOrEmpty(mood) ? mood : throw new StatusDomainException(nameof(mood));

            Text = !string.IsNullOrEmpty(text) ? text : throw new StatusDomainException(nameof(text));
        }
    }
}
