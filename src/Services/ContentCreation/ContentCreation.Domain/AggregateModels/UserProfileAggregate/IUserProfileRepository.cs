using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.Interfaces;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        Task<UserProfile> FindAsync(Guid userId);

        void AddAsync(UserProfile userProfile);

        void Delete(UserProfile userProfile);
    }
}