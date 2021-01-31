using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.Interfaces;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        void AddAsync(UserProfile userProfile);

        void Update(UserProfile userProfile);

        void Delete(UserProfile userProfile);
    }
}
