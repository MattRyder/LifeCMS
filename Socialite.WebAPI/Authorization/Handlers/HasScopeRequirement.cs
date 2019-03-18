using Microsoft.AspNetCore.Authorization;

namespace Socialite.WebAPI.Authorization.Handlers
{
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public string Scope { get; private set; }

        public string Issuer { get; private set; }

        public HasScopeRequirement(string scope, string issuer)
        {
            Scope = scope;
            Issuer = issuer;
        }
    }
}