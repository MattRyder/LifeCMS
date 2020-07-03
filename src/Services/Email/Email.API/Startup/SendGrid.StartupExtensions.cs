using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LifeCMS.Services.Email.Infrastructure.Interfaces;
using LifeCMS.Services.Email.Infrastructure.Smtp;
using System.Net.Mail;

namespace LifeCMS.Services.Email.API
{
    public static partial class StartupExtensions
    {
        public static void AddSendGrid(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEmailClient>(sp =>
            {
                var apiKey = configuration["Email:SendGrid:ApiKey"];

                var fromEmailAddress = new MailAddress(
                    configuration["Email:SendGrid:From:EmailAddress"],
                    configuration["Email:SendGrid:From:Name"]
                );

                return new SendGridEmailClient(apiKey, fromEmailAddress);
            });
        }
    }
}