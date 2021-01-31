using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Policies;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Domain.Interfaces;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LifeCMS.Services.ContentCreation.API.Services.Lookup
{
    public class LookupService<T> : ILookupService<T>
        where T : BaseEntity, IAggregateRoot
    {
        private readonly IRepository<T> _repository;

        private readonly IUserAccessor _userAccessor;

        private readonly IAuthorizationService _authorizationService;

        public LookupService(
            IRepository<T> repository,
            IUserAccessor userAccessor,
            IAuthorizationService authorizationService)
        {
            _repository = repository;

            _userAccessor = userAccessor;

            _authorizationService = authorizationService;
        }

        public async Task<T> FindEntityByIdAsync(Guid id)
        {
            var entity = await FindEntityAsync(id);

            await ValidateAccess(entity);

            return entity;
        }

        private async Task<T> FindEntityAsync(Guid id)
        {
            var entity = await _repository.FindAsync(id);

            if (entity is null)
            {
                throw new LookupServiceException(
                    $"Failed to find the {typeof(T).Name} for the given Id."
                );
            }

            return entity;
        }

        private async Task<bool> ValidateAccess(T entity)
        {
            var ownsResource = await _authorizationService.AuthorizeAsync(
                _userAccessor.User,
                entity,
                UserOwnsResourcePolicy.Name
            );

            return ownsResource.Succeeded
                ? true
                : throw new LookupServiceException(
                    $"Failed to validate access to the ${typeof(T).Name} entity."
                );
        }
    }
}
