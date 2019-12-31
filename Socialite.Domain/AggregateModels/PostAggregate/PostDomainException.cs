using System;
using System.Runtime.Serialization;

namespace Socialite.Domain.Exceptions
{
    public class PostDomainException : Exception
    {
        public PostDomainException(string message) : base(message)
        {
        }
    }
}