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

        private UserProfile _userProfile;

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
            _userProfile = await _lookupService
                .FindEntityByIdAsync(request.Id);

            UpdateName(request.Name);

            UpdateEmailAddress(request.EmailAddress);

            UpdateBio(request.Bio);

            UpdateLocation(request.Location);

            UpdateOccupation(request.Occupation);

            UpdateAvatarUrn(request.AvatarImageUrn);

            UpdateHeaderUrn(request.HeaderImageUrn);

            _userProfileRepository.Update(_userProfile);

            var saved = await _userProfileRepository
                .UnitOfWork
                .SaveEntitiesAsync();

            return new BasicResponse()
            {
                Success = saved
            };
        }

        private void UpdateName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _userProfile.UpdateName(name);
            }
        }

        private void UpdateEmailAddress(EmailAddress emailAddress)
        {
            _userProfile.UpdateEmailAddress(emailAddress);
        }

        private void UpdateBio(string bio)
        {
            _userProfile.UpdateBio(bio);
        }

        private void UpdateLocation(string location)
        {
            _userProfile.UpdateLocation(location);
        }

        private void UpdateOccupation(string occupation)
        {
            _userProfile.UpdateLocation(occupation);
        }

        private void UpdateAvatarUrn(string avatarImageUrn)
        {
            var willDelete = !string.IsNullOrEmpty(_userProfile.AvatarImageUrn)
                && !_userProfile.AvatarImageUrn.Equals(avatarImageUrn);

            if (willDelete)
            {
                _deleteFileService.DeleteFileAsync(_userProfile.AvatarImageUrn);
            }

            _userProfile.UpdateAvatarUrn(avatarImageUrn);
        }

        private void UpdateHeaderUrn(string headerImageUrn)
        {
            var willDelete = !string.IsNullOrEmpty(_userProfile.HeaderImageUrn)
                && !_userProfile.HeaderImageUrn.Equals(headerImageUrn);

            if (willDelete)
            {
                _deleteFileService.DeleteFileAsync(_userProfile.HeaderImageUrn);
            }

            _userProfile.UpdateHeaderImageUrn(headerImageUrn);
        }
    }
}
