using System;

namespace Socialite.Domain.Exceptions
{
    public class StatusDomainException : Exception
    {
        public StatusDomainException()
        {
        }

        public StatusDomainException(string message) : base(message)
        {
        }

        public StatusDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}