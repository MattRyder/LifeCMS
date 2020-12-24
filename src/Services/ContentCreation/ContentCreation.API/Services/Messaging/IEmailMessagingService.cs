using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.API.Services.Messaging
{
    public enum EmailTemplate
    {
        Hero
    }

    public interface IEmailMessagingService
    {
        void DispatchEmail(
            EmailTemplate template,
            EmailAddress fromEmailAddress,
            EmailAddress toAddress,
            string subject,
            object dataModel);
    }
}
