using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Services;

namespace LifeCMS.Services.ContentCreation.API.Startup
{
    public static partial class StartupExtensions
    {
        public static void AddLifeCMSAws(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<S3ImageUploadOptions>(configuration.GetSection("AmazonS3"));

            services.AddDefaultAWSOptions(configuration.GetAWSOptions());

            services.AddAWSService<IAmazonS3>();
        }
    }
}
