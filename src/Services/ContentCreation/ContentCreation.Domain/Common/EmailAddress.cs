using System;
using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.Common
{
    public class EmailAddress : ValueObject
    {
        public string Value { get; private set; }

        public EmailAddress(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
