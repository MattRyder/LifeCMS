using System.Threading;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands
{
    public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, bool>
    {
        private readonly IUserProfileRepository _userProfileRepository;

        private readonly ILogger<CreateUserProfileCommandHandler> _logger;

        private readonly IUserAccessor _userAccessor;

        public CreateUserProfileCommandHandler(
            IUserProfileRepository userProfileRepository,
            ILogger<CreateUserProfileCommandHandler> logger,
            IUserAccessor userAccessor
        )
        {
            _userProfileRepository = userProfileRepository;

            _logger = logger;

            _userAccessor = userAccessor;
        }

        public Task<bool> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userProfile = new UserProfile(_userAccessor.Id, request.Name, request.EmailAddress);

                _userProfileRepository.AddAsync(userProfile);

                return _userProfileRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (UserProfileDomainException ex)
            {
                _logger.LogError(ex, null);

                return Task.FromResult(false);
            }
        }
    }
}