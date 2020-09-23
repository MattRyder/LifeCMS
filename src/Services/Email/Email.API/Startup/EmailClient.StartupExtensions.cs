using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LifeCMS.Services.Email.Infrastructure.Interfaces;
using LifeCMS.Services.Email.Infrastructure.Smtp;
using System.Net.Mail;
using System.Net;
using System.Security;
using System;

namespace LifeCMS.Services.Email.API
{
    public static partial class StartupExtensions
    {
        public static void AddEmailClient(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var emailProvider = configuration["Email:Provider"];

            switch (emailProvider)
            {
                case "Smtp":
                    AddSmtp(services, configuration);
                    break;
                case "SendGrid":
                    AddSendGrid(services, configuration);
                    break;
                default:
                    throw new ArgumentException(
                        "No email provider has been configured."
                    );
            }
        }

        private static void AddSmtp(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var host = configuration.GetValue<string>("Email:Smtp:Host");

            var port = configuration.GetValue<int>("Email:Smtp:Port");

            var credential = new NetworkCredential(
                configuration.GetValue<string>("Email:Smtp:Username"),
                configuration.GetValue<SecureString>("Email:Smtp:Password")
            );

            var enableSsl = configuration.GetValue<bool>("Email:Smtp:EnableSsl");

            var timeout = configuration.GetValue<int>("Email:Smtp:Timeout");

            services.AddSingleton<IEmailClient>(sp =>
            {
                return new SmtpEmailClient(
                    host,
                    port,
                    credential,
                    enableSsl,
                    timeout
                );
            });
        }

        private static void AddSendGrid(
            this IServiceCollection services,
            IConfiguration configuration)
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
