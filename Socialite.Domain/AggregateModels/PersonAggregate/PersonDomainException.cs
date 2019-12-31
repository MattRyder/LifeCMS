using System;

namespace Socialite.Domain.AggregateModels.PersonAggregate
{
    public class PersonDomainException : Exception
    {
        public PersonDomainException(string message) : base(message)
        {
        }
    }
}