using System;
using System.Threading.Tasks;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.UserProfiles
{
    public interface IUserProfileQueries
    {
        Task<UserProfileViewModel> FindUserProfile(Guid userId);
    }
}
