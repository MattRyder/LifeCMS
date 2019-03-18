using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Socialite.WebAPI.Authorization.Handlers
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            const string SCOPE_CLAIM_TYPE = "scope";

            if(!context.User.HasClaim(c => c.Type == SCOPE_CLAIM_TYPE && c.Issuer == requirement.Issuer))
            {
                return Task.CompletedTask;
            }

            var scopes = context.User.FindFirst(c => c.Type == SCOPE_CLAIM_TYPE && c.Issuer == requirement.Issuer).Value.Split(',');

            if(scopes.Any(s => s == requirement.Scope))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}