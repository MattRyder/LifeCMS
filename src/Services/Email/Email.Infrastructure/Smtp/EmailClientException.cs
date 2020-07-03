using System;

namespace LifeCMS.Services.Email.Infrastructure.Smtp
{
    public class EmailClientException : Exception
    {
        public EmailClientException(string message) : base(message) { }
    }
}