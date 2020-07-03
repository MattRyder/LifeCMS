using LifeCMS.Services.Email.Domain.Concrete;

namespace LifeCMS.Services.Email.Infrastructure.Interfaces
{
    public interface IEmailClient
    {
        void Send(EmailMessage emailMessage);
    }
}