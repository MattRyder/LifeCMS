using System.Net.Mail;

namespace LifeCMS.Services.Email.Infrastructure.Interfaces
{
    public interface IEmailClient
    {
        void Send(MailMessage mailMessage);
    }
}
