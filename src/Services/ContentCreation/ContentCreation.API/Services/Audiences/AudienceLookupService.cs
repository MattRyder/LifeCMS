using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Authorization;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LifeCMS.Services.ContentCreation.API.Services.Audiences
{
    public class AudienceLookupServiceException : Exception
    {
        public AudienceLookupServiceException(string message) : base(message) { }
    }

    public class AudienceLookupService : IAudienceLookupService
    {
        private readonly IAudienceRepository _audienceRepository;

        private readonly IUserAccessor _userAccessor;

        private readonly IAuthorizationService _authorizationService;

        public AudienceLookupService(
            IAudienceRepository audienceRepository,
            IUserAccessor userAccessor,
            IAuthorizationService authorizationService
        )
        {
            _audienceRepository = audienceRepository;

            _userAccessor = userAccessor;

            _authorizationService = authorizationService;
        }

        public async Task<Audience> FindAudienceAsync(Guid audienceId)
        {
            var audience = await _audienceRepository.FindAsync(audienceId);

            if (audience is null)
            {
                throw new AudienceLookupServiceException(
                    "Failed to find the Audience for the given Audience Id"
                );
            }

            var ownsResource = await _authorizationService.OwnsResource(
                _userAccessor.User,
                audience
            );

            return ownsResource
            ? audience
            : throw new AudienceLookupServiceException(
                "Failed to validate ownership of the Audience."
            );

        }
    }
}
