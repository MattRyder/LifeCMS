using System;

namespace Socialite.Domain.AggregateModels.UsersAggregate
{
    public class UserDomainException : Exception
    {
        public UserDomainException(string message) : base(message)
        {
        }
    }
}