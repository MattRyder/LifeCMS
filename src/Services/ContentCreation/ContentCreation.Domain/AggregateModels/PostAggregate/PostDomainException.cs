using System;

namespace LifeCMS.Services.ContentCreation.Domain.Exceptions
{
    public class PostDomainException : Exception
    {
        public PostDomainException(string message) : base(message)
        {
        }
    }
}