using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LifeCMS.Services.ContentCreation.API.Authorization.Requirements;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;

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

            var shouldSucceedContext = false;

            switch (context.Resource)
            {
                case UserProfile userProfile:
                    shouldSucceedContext = userProfile.UserId.Equals(userId);
                    break;
                case Newsletter newsletter:
                    shouldSucceedContext = newsletter.UserId.Equals(userId);
                    break;
            }

            if (shouldSucceedContext)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}