using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Policies;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.UserProfiles
{
    public class DeleteUserProfileCommandHandler : IRequestHandler<DeleteUserProfileCommand, bool>
    {
        private readonly IUserProfileRepository _userProfileRepository;

        private readonly IAuthorizationService _authorizationService;

        private readonly IUserAccessor _userAccessor;

        private readonly ILogger<DeleteUserProfileCommandHandler> _logger;

        public DeleteUserProfileCommandHandler(
            IUserProfileRepository userProfileRepository,
            IAuthorizationService authorizationService,
            IUserAccessor userAccessor,
            ILogger<DeleteUserProfileCommandHandler> logger
            )
        {
            _userProfileRepository = userProfileRepository;

            _authorizationService = authorizationService;

            _userAccessor = userAccessor;

            _logger = logger;
        }

        public async Task<bool> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userProfile = await _userProfileRepository.FindAsync(request.Id);

                var ownsResource = await OwnsResource(userProfile);

                if (!ownsResource)
                {
                    throw new UnauthorizedAccessException();
                }

                _userProfileRepository.Delete(userProfile);

                return await _userProfileRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex) when (
                ex is UserProfileDomainException ||
                ex is UnauthorizedAccessException
            )
            {
                _logger.LogError(null, ex, ex.Message);

                return false;
            }
        }

        private async Task<bool> OwnsResource(UserProfile userProfile)
        {
            var ownsResource = await _authorizationService.AuthorizeAsync(
                _userAccessor.User,
                userProfile,
                UserOwnsResourcePolicy.Name
            );

            return ownsResource.Succeeded;
        }
    }
}