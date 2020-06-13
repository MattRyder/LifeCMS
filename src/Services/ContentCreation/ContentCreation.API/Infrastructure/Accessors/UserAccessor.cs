using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using IdentityModel;
using System;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Accessors
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext.User;

        public Guid Id => new Guid(User.FindFirstValue(JwtClaimTypes.Subject));

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}