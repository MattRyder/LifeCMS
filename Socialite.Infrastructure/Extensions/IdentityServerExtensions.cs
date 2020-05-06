using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Socialite.Domain.Common;

namespace Socialite.Infrastructure.Exensions
{
    public static class IdentityServerExtensions
    {
        public static IIdentityServerBuilder LoadSigningCredential(this IIdentityServerBuilder builder, string certificatePath)
        {
            if (!string.IsNullOrEmpty(certificatePath))
            {
                var signingCertificate = new X509Certificate2(certificatePath);

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