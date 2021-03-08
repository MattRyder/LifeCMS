using LifeCMS.Services.ContentCreation.API.Application.Commands.Newsletters;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Newsletters;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;
using LifeCMS.Services.ContentCreation.Domain.Interfaces;
using LifeCMS.Services.ContentCreation.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LifeCMS.Services.ContentCreation.API.Startup
{
    public static partial class WebApiStartupExtensions
    {
        public static void AddWebApiTemplates(this IServiceCollection services)
        {
            services.AddTransient<INewsletterRepository, NewsletterRepository>()
                    .AddTransient<IRepository<Newsletter>, NewsletterRepository>()
                    .AddTransient<IRequestHandler<CreateNewsletterCommand, bool>, CreateNewsletterCommandHandler>()
                    .AddTransient<IRequestHandler<UpdateNewsletterBodyCommand, bool>, UpdateNewsletterBodyCommandHandler>()
                    .AddTransient<IRequestHandler<DeleteNewsletterCommand, bool>, DeleteNewsletterCommandHandler>()
                    .AddTransient<INewsletterQueries, NewsletterQueries>();
        }
    }
}
