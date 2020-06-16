using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        public ContentCreationDbContext DbContext { get; private set; }
        public IUnitOfWork UnitOfWork => DbContext;

        public UserProfileRepository(ContentCreationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<UserProfile> FindAsync(Guid userId)
        {
            return await DbContext.Set<UserProfile>().FindAsync(userId);
        }

        public async void AddAsync(UserProfile userProfile)
        {
            await DbContext.Set<UserProfile>().AddAsync(userProfile);
        }

        public void Delete(UserProfile userProfile)
        {
            DbContext.Set<UserProfile>().Remove(userProfile);
        }
    }
}