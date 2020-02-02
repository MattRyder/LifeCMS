using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Socialite.WebAPI.Infrastructure.Services;

namespace Socialite.WebAPI.Startup
{
    public static partial class StartupExtensions
    {
        public static void AddSocialiteAws(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<S3ImageUploadOptions>(configuration.GetSection("AmazonS3"));

            services.AddDefaultAWSOptions(configuration.GetAWSOptions());

            services.AddAWSService<IAmazonS3>();
        }
    }
}