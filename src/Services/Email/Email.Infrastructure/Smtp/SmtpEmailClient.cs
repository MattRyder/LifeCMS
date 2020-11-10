using System.Net;
using System.Net.Mail;
using LifeCMS.Services.Email.Infrastructure.Interfaces;

namespace LifeCMS.Services.Email.Infrastructure.Smtp
{
    public class SmtpEmailClient : IEmailClient
    {
        private readonly SmtpClient _smtpClient;

        public SmtpEmailClient(
            string host,
            int port,
            NetworkCredential credentials,
            bool enableSsl,
            int timeout
        )
        {
            _smtpClient = new SmtpClient()
            {
                Credentials = credentials,
                Host = host,
                Port = port,
                EnableSsl = enableSsl,
                Timeout = timeout
            };
        }

        public void Send(MailMessage mailMessage)
        {
            _smtpClient.SendAsync(mailMessage, mailMessage.ToString());
        }
    }
}
