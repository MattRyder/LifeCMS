using System;

namespace Socialite.Domain.Exceptions
{
    public class StatusDomainException : Exception
    {
        public StatusDomainException(string message) : base(message)
        {
        }
    }
}