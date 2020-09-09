using System.Security.Claims;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Policies;
using LifeCMS.Services.ContentCreation.Domain.Common;
using Microsoft.AspNetCore.Authorization;

namespace LifeCMS.Services.ContentCreation.API.Authorization
{
    public static class AuthorizationServiceExtensions
    {
        public static async Task<bool> OwnsResource<T>(
            this IAuthorizationService authorizationService,
            ClaimsPrincipal user,
            T resource
        ) where T : BaseEntity
        {
            var ownsResource = await authorizationService.AuthorizeAsync(
                user,
                resource,
                UserOwnsResourcePolicy.Name
            );

            return ownsResource.Succeeded;
        }
    }
}