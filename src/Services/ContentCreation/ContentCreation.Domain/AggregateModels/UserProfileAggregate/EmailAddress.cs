using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate
{
    public class EmailAddress : ValueObject
    {
        public string Value { get; private set; }

        public EmailAddress(string value)
        {
            Value = value ?? throw new UserProfileDomainException(nameof(value));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}