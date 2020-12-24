using System;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.API.Services.Audiences
{
    public interface ISubscriberEmailService
    {
        void SendEmail(
            Guid audienceId,
            EmailAddress emailAddress,
            string name,
            string subscriberToken);
    }
}
