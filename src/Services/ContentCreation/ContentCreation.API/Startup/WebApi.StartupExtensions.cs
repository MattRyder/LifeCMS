using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Albums;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Posts;
using LifeCMS.Services.ContentCreation.API.Application.Commands.Statuses;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Albums;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Posts;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Statuses;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.StatusAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;
using LifeCMS.Services.ContentCreation.Infrastructure.Repositories;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Extensions;
using System;

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

            services.AddTransient<IStatusRepository, StatusRepository>()
                    .AddTransient<IRequestHandler<CreateStatusCommand, BasicResponse>, CreateStatusCommandHandler>()
                    .AddTransient<IStatusQueries, StatusQueries>();

            services.AddTransient<IPostRepository, PostRepository>()
                    .AddTransient<IRequestHandler<CreatePostCommand, bool>, CreatePostCommandHandler>()
                    .AddTransient<IPostQueries, PostQueries>();

            services.AddTransient<IAlbumRepository, AlbumRepository>()
                    .AddTransient<IRequestHandler<CreateAlbumCommand, bool>, CreateAlbumCommandHandler>()
                    .AddTransient<IRequestHandler<UploadPhotoCommand, bool>, UploadPhotoCommandHandler>()
                    .AddTransient<IAlbumQueries, AlbumQueries>();
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