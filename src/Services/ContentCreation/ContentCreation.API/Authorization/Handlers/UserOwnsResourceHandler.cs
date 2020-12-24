using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LifeCMS.Services.ContentCreation.API.Authorization.Requirements;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;
using System;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;

namespace LifeCMS.Services.ContentCreation.API.Authorization.Handlers
{
    public class UserOwnsResourceException : Exception
    {
        public UserOwnsResourceException(string message) : base(message) { }
    }

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

            var shouldSucceedContext = context.Resource switch
            {
                UserProfile userProfile => userProfile.UserId.Equals(userId),
                Newsletter newsletter => newsletter.UserId.Equals(userId),
                Campaign campaign => campaign.UserId.Equals(userId),
                Audience audience => audience.UserId.Equals(userId),
                null => throw new UserOwnsResourceException("Resource must not be null."),
                _ => throw new UserOwnsResourceException("Resource not registered with this handler."),
            };

            if (shouldSucceedContext)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
