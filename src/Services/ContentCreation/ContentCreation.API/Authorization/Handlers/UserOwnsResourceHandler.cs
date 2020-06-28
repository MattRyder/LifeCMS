using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LifeCMS.Services.ContentCreation.API.Authorization.Requirements;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;

namespace LifeCMS.Services.ContentCreation.API.Authorization.Handlers
{
    public class UserOwnsResourceHandler : AuthorizationHandler<UserOwnsResourceRequirement>
    {
        private readonly IUserAccessor _userAccessor;

        public UserOwnsResourceHandler(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            UserOwnsResourceRequirement requirement
        )
        {
            var userId = _userAccessor.Id;

            if (context.Resource is UserProfile userProfile && userProfile.UserId.Equals(userId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}