using Microsoft.AspNetCore.Authorization;

namespace LifeCMS.WebAPI.Authorization.Requirements
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