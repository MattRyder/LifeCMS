using System;
using System.Security.Claims;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Interfaces
{
    public interface IUserAccessor
    {
        ClaimsPrincipal User { get; }

        Guid Id { get; }
    }
}