using Dapper;
using MediatR;
using Amazon.S3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Extensions;
using LifeCMS.Services.ContentCreation.Infrastructure.Accessors;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Dapper;
using LifeCMS.Services.ContentCreation.API.Authorization.Handlers;
using LifeCMS.Services.ContentCreation.Infrastructure.Services;
using LifeCMS.Services.ContentCreation.Infrastructure.Services.Aws;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Files;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using LifeCMS.Services.ContentCreation.API.Services.Messaging;
using LifeCMS.Services.ContentCreation.API.Services.Lookup;
using LifeCMS.Services.ContentCreation.Infrastructure.Services.DeleteFileService;

namespace LifeCMS.Services.ContentCreation.API.Startup
{
    public static partial class StartupExtensions
    {
        public static void AddLifeCMSWebApi(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = ConfigurationHelper.GetConnectionString(
                configuration,
                "LifeCMS");

            services
            .AddTransient<IDbConnectionFactory, MySqlDbConnectionFactory>(f =>
            {
                return new MySqlDbConnectionFactory(connectionString);
            })
            .AddDbContext<ContentCreationDbContext>(opts =>
            {
                opts.UseMySql(
                    connectionString: connectionString,
                    serverVersion: ServerVersion.AutoDetect(connectionString));
            });

            AddWebApiComponents(services, configuration);

            SqlMapper.AddTypeHandler(new UriTypeHandler());

            services.AddTransient<IRequestHandler<CreatePresignedPostUrlCommand, BasicResponse>, CreatePresignedPostUrlCommandHandler>();

            services.AddSingleton<IPresignedPostService>(x =>
            {
                var bucketName = ConfigurationHelper.GetConfigurationValue(
                    configuration,
                    "AWS:BucketName");

                return new S3PresignedPostService(
                    x.GetRequiredService<IAmazonS3>(),
                    bucketName);
            });

            services.AddSingleton<IDeleteFileService, S3DeleteFileService>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IUserAccessor, UserAccessor>();

            services.AddSingleton<IAuthorizationHandler, UserOwnsResourceHandler>();

            services.AddTransient<IEmailMessagingService, EmailMessagingService>();

            services.AddScoped(typeof(ILookupService<>), typeof(LookupService<>));

            services.AddTransient<IFileUriService, S3FileUriService>()
                    .AddTransient<IRequestHandler<CreateFileUrlCommand, BasicResponse>, CreateFileUrlCommandHandler>();
        }

        public static void UseLifeCMSWebApi(this IApplicationBuilder app)
        {
            app.ApplyMigrations<ContentCreationDbContext>();
        }

        private static void AddWebApiComponents(
            IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddWebApiAlbums();

            services.AddWebApiPosts();

            services.AddWebApiTemplates();

            services.AddWebApiUserProfiles();

            services.AddWebApiCampaigns();

            services.AddWebApiAudiences(configuration);
        }
    }
}
