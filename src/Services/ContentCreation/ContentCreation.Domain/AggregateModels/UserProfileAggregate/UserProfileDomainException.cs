using System;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate
{
    public class UserProfileDomainException : Exception
    {
        public UserProfileDomainException(string message) : base(message) { }
    }
}