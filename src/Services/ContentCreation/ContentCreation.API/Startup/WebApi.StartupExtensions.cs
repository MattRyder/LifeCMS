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

namespace LifeCMS.Services.ContentCreation.API.Startup
{
    public static partial class StartupExtensions
    {
        public static void AddLifeCMSWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = GetConnectionString(configuration);

            services
            .AddTransient<IDbConnectionFactory, MySqlDbConnectionFactory>(f =>
            {
                return new MySqlDbConnectionFactory(connectionString);
            })
            .AddDbContext<ContentCreationDbContext>(opts =>
            {
                opts.UseMySql(connectionString);

                opts.EnableSensitiveDataLogging();
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
                    .AddTransient<IRequestHandler<DeleteNewsletterCommand, bool>, DeleteNewsletterCommandHandler>()
                    .AddTransient<INewsletterQueries, NewsletterQueries>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IUserAccessor, UserAccessor>();

            services.AddSingleton<IAuthorizationHandler, UserOwnsResourceHandler>();
        }

        public static void UseLifeCMSWebApi(this IApplicationBuilder app)
        {
            app.ApplyMigrations<ContentCreationDbContext>();
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LifeCMS");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("The connection string `LifeCMS' was not provided.");
            }

            return connectionString;
        }
    }
}