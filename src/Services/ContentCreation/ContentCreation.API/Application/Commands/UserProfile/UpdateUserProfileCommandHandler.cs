using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Services.Lookup;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using LifeCMS.Services.ContentCreation.Infrastructure.Services.DeleteFileService;
using MediatR;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.UserProfiles
{
    public class UpdateUserProfileCommandHandler
        : IRequestHandler<UpdateUserProfileCommand, BasicResponse>
    {
        private readonly IUserProfileRepository _userProfileRepository;

        private readonly ILookupService<UserProfile> _lookupService;

        private readonly IDeleteFileService _deleteFileService;

        public UpdateUserProfileCommandHandler(
            IUserProfileRepository userProfileRepository,
            ILookupService<UserProfile> lookupService,
            IDeleteFileService deleteFileService)
        {
            _userProfileRepository = userProfileRepository;

            _lookupService = lookupService;

            _deleteFileService = deleteFileService;
        }

        public async Task<BasicResponse> Handle(
            UpdateUserProfileCommand request,
            CancellationToken cancellationToken)
        {
            var userProfile = await _lookupService
                .FindEntityByIdAsync(request.Id);

            userProfile = UpdateName(userProfile, request.Name);

            userProfile = UpdateEmailAddress(userProfile, request.EmailAddress);

            userProfile = UpdateBio(userProfile, request.Bio);

            userProfile = UpdateLocation(userProfile, request.Location);

            userProfile = UpdateOccupation(userProfile, request.Occupation);

            userProfile = UpdateAvatarUrn(userProfile, request.AvatarImageUrn);

            _userProfileRepository.Update(userProfile);

            var saved = await _userProfileRepository.UnitOfWork.SaveEntitiesAsync();

            return new BasicResponse()
            {
                Success = saved
            };
        }

        private UserProfile UpdateName(
            UserProfile userProfile,
            string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                userProfile.UpdateName(name);
            }

            return userProfile;
        }

        private UserProfile UpdateEmailAddress(
            UserProfile userProfile,
            EmailAddress emailAddress)
        {
            userProfile.UpdateEmailAddress(emailAddress);

            return userProfile;
        }

        private UserProfile UpdateBio(
            UserProfile userProfile,
            string bio)
        {
            userProfile.UpdateBio(bio);

            return userProfile;
        }

        private UserProfile UpdateLocation(
            UserProfile userProfile,
            string location)
        {
            userProfile.UpdateLocation(location);

            return userProfile;
        }

        private UserProfile UpdateOccupation(
            UserProfile userProfile,
            string occupation)
        {
            userProfile.UpdateLocation(occupation);

            return userProfile;
        }

        private UserProfile UpdateAvatarUrn(
            UserProfile userProfile,
            string avatarImageUrn)
        {
            var willDelete = !string.IsNullOrEmpty(userProfile.AvatarImageUrn)
                && !userProfile.AvatarImageUrn.Equals(avatarImageUrn);

            if (willDelete)
            {
                _deleteFileService.DeleteFileAsync(userProfile.AvatarImageUrn);
            }

            userProfile.UpdateAvatarUrn(avatarImageUrn);

            return userProfile;
        }
    }
}
