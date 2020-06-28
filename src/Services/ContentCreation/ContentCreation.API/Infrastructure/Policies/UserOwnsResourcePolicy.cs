using LifeCMS.Services.ContentCreation.API.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace LifeCMS.Services.ContentCreation.API.Infrastructure.Policies
{
    public class UserOwnsResourcePolicy
    {
        public const string Name = "UserOwnsResourcePolicy";

        public static AuthorizationPolicy GetPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .AddRequirements(new UserOwnsResourceRequirement())
                .Build();
        }
    }
}