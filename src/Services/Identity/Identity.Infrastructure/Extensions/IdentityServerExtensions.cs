using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;

namespace LifeCMS.Services.Identity.Infrastructure.Exensions
{
    public static class IdentityServerExtensions
    {
        public static IIdentityServerBuilder LoadSigningCredential(this IIdentityServerBuilder builder, string encodedCertificate)
        {
            if (!string.IsNullOrEmpty(encodedCertificate))
            {
                var certificateBytes = Convert.FromBase64String(encodedCertificate);

                var signingCertificate = new X509Certificate2(certificateBytes);

                builder.AddSigningCredential(signingCertificate);
            }
            else
            {
                builder.AddDeveloperSigningCredential();
            }

            return builder;
        }
    }
}