using LifeCMS.Services.ContentCreation.API.Application.Commands.Audiences;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Audiences;
using LifeCMS.Services.ContentCreation.API.Services.Audiences;
using LifeCMS.Services.ContentCreation.API.Services.Messaging;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Infrastructure.Repositories;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LifeCMS.Services.ContentCreation.API.Startup
{
    public static partial class WebApiStartupExtensions
    {
        public static void AddWebApiAudiences(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IAudienceRepository, AudienceRepository>()
                    .AddTransient<IAudienceLookupService, AudienceLookupService>()
                    .AddTransient<IRequestHandler<CreateAudienceCommand, BasicResponse>, CreateAudienceCommandHandler>()
                    .AddTransient<IRequestHandler<UpdateAudienceNameCommand, BasicResponse>, UpdateAudienceNameCommandHandler>()
                    .AddTransient<IRequestHandler<DeleteAudienceCommand, BasicResponse>, DeleteAudienceCommandHandler>()
                    .AddTransient<IRequestHandler<ConfirmSubscriberCommand, BasicResponse>, ConfirmSubscriberCommandHandler>()
                    .AddTransient<IAudienceQueries, AudienceQueries>();

            services.AddSingleton<ISubscriberEmailService>(x =>
            {
                var fromEmailAddress = ConfigurationHelper.GetConfigurationValue(
                        configuration,
                        "ContentCreation:Email:FromEmailAddress");

                var webSpaHost = ConfigurationHelper.GetConfigurationValue(
                        configuration,
                        "Host:WebSpa");

                return new SubscriberEmailService(
                    x.GetRequiredService<IEmailMessagingService>(),
                        new EmailAddress(fromEmailAddress),
                        webSpaHost);
            });
        }
    }
}
