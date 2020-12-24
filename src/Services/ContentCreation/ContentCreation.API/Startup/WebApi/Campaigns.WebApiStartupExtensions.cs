using LifeCMS.Services.ContentCreation.API.Application.Commands.Campaigns;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Campaigns;
using LifeCMS.Services.ContentCreation.API.Services.Campaigns;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LifeCMS.Services.ContentCreation.API.Startup
{
    public static partial class WebApiStartupExtensions
    {
        public static void AddWebApiCampaigns(this IServiceCollection services)
        {
            services.AddTransient<ICampaignRepository, CampaignRepository>()
                    .AddTransient<IRequestHandler<CreateCampaignCommand, bool>, CreateCampaignCommandHandler>()
                    .AddTransient<IRequestHandler<DeleteCampaignCommand, bool>, DeleteCampaignCommandHandler>()
                    .AddTransient<IRequestHandler<UpdateCampaignNameCommand, bool>, UpdateCampaignNameCommandHandler>()
                    .AddTransient<IRequestHandler<UpdateCampaignSubjectCommand, bool>, UpdateCampaignSubjectCommandHandler>()
                    .AddTransient<ICampaignLookupService, CampaignLookupService>()
                    .AddTransient<ICampaignValidationService, CampaignValidationService>()
                    .AddTransient<ICampaignDeliveryService, CampaignDeliveryService>()
                    .AddTransient<ICampaignQueries, CampaignQueries>();
        }
    }
}
