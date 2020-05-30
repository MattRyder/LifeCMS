using System;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.PersonAggregate
{
    public class PersonDomainException : Exception
    {
        public PersonDomainException(string message) : base(message)
        {
        }
    }
}