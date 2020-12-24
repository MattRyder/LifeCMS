using LifeCMS.Services.ContentCreation.API.Application.Commands.UserProfiles;
using LifeCMS.Services.ContentCreation.API.Application.Queries.UserProfiles;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LifeCMS.Services.ContentCreation.API.Startup
{
    public static partial class WebApiStartupExtensions
    {
        public static void AddWebApiUserProfiles(this IServiceCollection services)
        {

            services.AddTransient<IUserProfileRepository, UserProfileRepository>()
                    .AddTransient<IRequestHandler<CreateUserProfileCommand, bool>, CreateUserProfileCommandHandler>()
                    .AddTransient<IUserProfileQueries, UserProfileQueries>();
        }
    }
}
