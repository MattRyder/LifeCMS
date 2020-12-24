using LifeCMS.Services.ContentCreation.API.Application.Commands.Posts;
using LifeCMS.Services.ContentCreation.API.Application.Events;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Posts;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.Domain.Events.Posts;
using LifeCMS.Services.ContentCreation.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LifeCMS.Services.ContentCreation.API.Startup
{
    public static partial class WebApiStartupExtensions
    {
        public static void AddWebApiPosts(this IServiceCollection services)
        {
            services.AddTransient<IPostRepository, PostRepository>()
                         .AddTransient<IRequestHandler<CreatePostCommand, bool>, CreatePostCommandHandler>()
                         .AddTransient<INotificationHandler<PostPublishedEvent>, PostPublishedEventHandler>()
                         .AddTransient<IPostQueries, PostQueries>();
        }
    }
}
