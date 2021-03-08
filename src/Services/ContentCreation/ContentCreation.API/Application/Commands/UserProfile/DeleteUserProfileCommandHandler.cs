using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Services.Lookup;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Responses;
using LifeCMS.Services.ContentCreation.Infrastructure.Services.DeleteFileService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.UserProfiles
{
    public class DeleteUserProfileCommandHandler
        : IRequestHandler<DeleteUserProfileCommand, BasicResponse>
    {
        private readonly IUserProfileRepository _userProfileRepository;

        private readonly IDeleteFileService _deleteFileService;

        private readonly ILookupService<UserProfile> _lookupService;

        private readonly ILogger<DeleteUserProfileCommandHandler> _logger;

        public DeleteUserProfileCommandHandler(
            IUserProfileRepository userProfileRepository,
            IDeleteFileService deleteFileService,
            ILookupService<UserProfile> lookupService,
            ILogger<DeleteUserProfileCommandHandler> logger)
        {
            _userProfileRepository = userProfileRepository;

            _deleteFileService = deleteFileService;

            _lookupService = lookupService;

            _logger = logger;
        }

        public async Task<BasicResponse> Handle(
            DeleteUserProfileCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var userProfile = await _lookupService
                    .FindEntityByIdAsync(request.Id);

                _userProfileRepository.Delete(userProfile);

                DeleteImageAsync(userProfile.AvatarImageUrn);

                DeleteImageAsync(userProfile.HeaderImageUrn);

                var result = await _userProfileRepository
                    .UnitOfWork
                    .SaveEntitiesAsync();

                return new BasicResponse()
                {
                    Success = true
                };
            }
            catch (LookupServiceException ex)
            {
                _logger.LogError(null, ex.Message);

                return new BasicResponse()
                {
                    Success = false,
                    Errors = new[] { ex.Message }
                };
            }
        }

        private async void DeleteImageAsync(string urn)
        {
            if (!string.IsNullOrEmpty(urn))
            {
                await _deleteFileService.DeleteFileAsync(urn);
            }
        }
    }
}
