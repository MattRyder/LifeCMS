using System;

namespace LifeCMS.Services.ContentCreation.Domain.Exceptions
{
    public class StatusDomainException : Exception
    {
        public StatusDomainException(string message) : base(message)
        {
        }
    }
}