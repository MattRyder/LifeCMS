using System;
using Socialite.Domain.Common;
using Socialite.Domain.Exceptions;
using Socialite.Domain.Interfaces;

namespace Socialite.Domain.AggregateModels.StatusAggregate
{
    public class Status : BaseEntity, IAggregateRoot
    {
        public string Mood { get; private set; }

        public string Text { get; private set; }

        public Status(string mood, string text)
        {
            Mood = !String.IsNullOrEmpty(mood) ? mood : throw new StatusDomainException(nameof(mood));
            Text = !String.IsNullOrEmpty(text) ? text : throw new StatusDomainException(nameof(text));
        }
    }
}
