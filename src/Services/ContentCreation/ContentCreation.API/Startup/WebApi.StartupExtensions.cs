using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Albums;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Posts;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Albums;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Posts;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;
using LifeCMS.Services.ContentCreation.Infrastructure.Repositories;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Extensions;
using System;
using LifeCMS.Services.ContentCreation.API.Application.Events;
using LifeCMS.Services.ContentCreation.Domain.Events.Posts;
using LifeCMS.Services.ContentCreation.Infrastructure.Accessors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.API.Application.Commands.UserProfiles;
using LifeCMS.Services.ContentCreation.API.Application.Queries.UserProfiles;
using Dapper;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Dapper;
using Microsoft.AspNetCore.Authorization;
using LifeCMS.Services.ContentCreation.API.Authorization.Handlers;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Newsletters;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Newsletters;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Campaigns;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Campaigns;
using LifeCMS.Services.ContentCreation.API.Services.Campaigns;
using LifeCMS.Services.ContentCreation.Infrastructure.Services.Aws;
using Amazon.S3;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Files;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;

namespace LifeCMS.Services.ContentCreation.API.Startup
{
    public static partial class StartupExtensions
    {
        public static void AddLifeCMSWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = GetConnectionString(
                configuration,
                "LifeCMS"
            );

            services
            .AddTransient<IDbConnectionFactory, MySqlDbConnectionFactory>(f =>
            {
                return new MySqlDbConnectionFactory(connectionString);
            })
            .AddDbContext<ContentCreationDbContext>(opts =>
            {
                opts.UseMySql(connectionString);
            });

            SqlMapper.AddTypeHandler(new UriTypeHandler());

            services.AddTransient<IPostRepository, PostRepository>()
                    .AddTransient<IRequestHandler<CreatePostCommand, bool>, CreatePostCommandHandler>()
                    .AddTransient<INotificationHandler<PostPublishedEvent>, PostPublishedEventHandler>()
                    .AddTransient<IPostQueries, PostQueries>();

            services.AddTransient<IAlbumRepository, AlbumRepository>()
                    .AddTransient<IRequestHandler<CreateAlbumCommand, bool>, CreateAlbumCommandHandler>()
                    .AddTransient<IRequestHandler<UploadPhotoCommand, bool>, UploadPhotoCommandHandler>()
                    .AddTransient<IAlbumQueries, AlbumQueries>();

            services.AddTransient<IUserProfileRepository, UserProfileRepository>()
                    .AddTransient<IRequestHandler<CreateUserProfileCommand, bool>, CreateUserProfileCommandHandler>()
                    .AddTransient<IUserProfileQueries, UserProfileQueries>();

            services.AddTransient<INewsletterRepository, NewsletterRepository>()
                    .AddTransient<IRequestHandler<CreateNewsletterCommand, bool>, CreateNewsletterCommandHandler>()
                    .AddTransient<IRequestHandler<UpdateNewsletterBodyCommand, bool>, UpdateNewsletterBodyCommandHandler>()
                    .AddTransient<IRequestHandler<DeleteNewsletterCommand, bool>, DeleteNewsletterCommandHandler>()
                    .AddTransient<INewsletterQueries, NewsletterQueries>();

            services.AddTransient<ICampaignRepository, CampaignRepository>()
                    .AddTransient<IRequestHandler<CreateCampaignCommand, bool>, CreateCampaignCommandHandler>()
                    .AddTransient<IRequestHandler<DeleteCampaignCommand, bool>, DeleteCampaignCommandHandler>()
                    .AddTransient<IRequestHandler<UpdateCampaignNameCommand, bool>, UpdateCampaignNameCommandHandler>()
                    .AddTransient<IRequestHandler<UpdateCampaignSubjectCommand, bool>, UpdateCampaignSubjectCommandHandler>()
                    .AddTransient<ICampaignLookupService, CampaignLookupService>()
                    .AddTransient<ICampaignValidationService, CampaignValidationService>()
                    .AddTransient<ICampaignDeliveryService, CampaignDeliveryService>()
                    .AddTransient<ICampaignQueries, CampaignQueries>();

            services.AddTransient<IRequestHandler<CreatePresignedPostUrlCommand, BasicResponse>, CreatePresignedPostUrlCommandHandler>();

            services.AddSingleton<IPresignedPostService>(x =>
            {
                return new S3PresignedPostService(
                    x.GetRequiredService<IAmazonS3>(),
                    GetConfigurationValue(configuration, "AWS:BucketName")
                );
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IUserAccessor, UserAccessor>();

            services.AddSingleton<IAuthorizationHandler, UserOwnsResourceHandler>();
        }

        public static void UseLifeCMSWebApi(this IApplicationBuilder app)
        {
            app.ApplyMigrations<ContentCreationDbContext>();
        }
    }
}
