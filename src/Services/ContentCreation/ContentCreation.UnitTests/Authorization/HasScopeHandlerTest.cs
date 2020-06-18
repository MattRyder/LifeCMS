using System.Security.Claims;
using LifeCMS.WebAPI.Authorization.Handlers;
using LifeCMS.WebAPI.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Authorization
{
    public class HasScopeHandlerTest
    {
        private readonly Mock<AuthorizationHandlerContext> _authorizationHandlerContextMock;

        public HasScopeHandlerTest()
        {
            _authorizationHandlerContextMock = new Mock<AuthorizationHandlerContext>();
        }

        [Fact]
        public void HandleRequirementAsync_ShouldSucceedContext_GivenValidRequirement()
        {
            var issuer = "http://example.com";

            var scopeClaimType = "scope";

            var scopes = "post:read";

            var requirements = new[] { new HasScopeRequirement(scopes, issuer) };

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[] { new Claim(scopeClaimType, scopes, null, issuer) }
                )
            );

            var authorizationHandlerContext = new AuthorizationHandlerContext(requirements, user, null);

            var handler = new HasScopeHandler();

            handler.HandleAsync(authorizationHandlerContext);

            Assert.True(authorizationHandlerContext.HasSucceeded);
        }

        [Fact]
        public void HandleRequirementAsync_ShouldNotSucceedContext_GivenInvalidIssuer()
        {
            var invalidIssuer = "http://contoso.com";

            var scopeClaimType = "scope";

            var scopes = "post:read";

            var requirements = new[] { new HasScopeRequirement(scopes, invalidIssuer) };

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[] { new Claim(scopeClaimType, scopes, null, "https://example.com") }
                )
            );

            var authorizationHandlerContext = new AuthorizationHandlerContext(requirements, user, null);

            var handler = new HasScopeHandler();

            handler.HandleAsync(authorizationHandlerContext);

            Assert.False(authorizationHandlerContext.HasSucceeded);
        }

        [Fact]
        public void HandleRequirementAsync_ShouldNotSucceedContext_GivenEmptyScope()
        {
            var issuer = "http://example.com";

            var scopeClaimType = "scope";

            var requirements = new[] { new HasScopeRequirement("peace:find", issuer) };

            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[] { new Claim(scopeClaimType, "", null, issuer) }
                )
            );

            var authorizationHandlerContext = new AuthorizationHandlerContext(requirements, user, null);

            var handler = new HasScopeHandler();

            handler.HandleAsync(authorizationHandlerContext);

            Assert.False(authorizationHandlerContext.HasSucceeded);
        }
    }
}