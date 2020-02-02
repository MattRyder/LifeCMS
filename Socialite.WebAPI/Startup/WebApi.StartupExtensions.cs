using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Socialite.Domain.AggregateModels.AlbumAggregate;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Infrastructure.Data;
using Socialite.Infrastructure.Repositories;
using Socialite.WebAPI.Application.Commands.Albums;
using Socialite.WebAPI.Application.Commands.Posts;
using Socialite.WebAPI.Application.Commands.Statuses;
using Socialite.WebAPI.Application.Queries.Albums;
using Socialite.WebAPI.Application.Queries.Posts;
using Socialite.WebAPI.Application.Responses;
using Socialite.WebAPI.Extensions;
using Socialite.WebAPI.Queries.Posts;
using Socialite.WebAPI.Queries.Statuses;

namespace Socialite.WebAPI.Startup
{
    public static partial class StartupExtensions
    {
        public static void AddSocialiteWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Socialite");

            services.AddDbContext<SocialiteDbContext>(opts => opts.UseMySql(connectionString));

            services.AddTransient<IStatusRepository, StatusRepository>()
                    .AddTransient<IRequestHandler<CreateStatusCommand, bool>, CreateStatusCommandHandler>()
                    .AddTransient<IStatusQueries, StatusQueries>();

            services.AddTransient<IPostRepository, PostRepository>()
                    .AddTransient<IRequestHandler<CreatePostCommand, bool>, CreatePostCommandHandler>()
                    .AddTransient<IPostQueries, PostQueries>();

            services.AddTransient<IAlbumRepository, AlbumRepository>()
                    .AddTransient<IRequestHandler<CreateAlbumCommand, bool>, CreateAlbumCommandHandler>()
                    .AddTransient<IRequestHandler<UploadPhotoCommand, bool>, UploadPhotoCommandHandler>()
                    .AddTransient<IAlbumQueries, AlbumQueries>();
        }

        public static void UseSocialiteWebApi(this IApplicationBuilder app)
        {
            app.ApplyMigrations<SocialiteDbContext>();
        }
    }
}