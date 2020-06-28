using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.UserProfiles
{
    public interface IUserProfileQueries
    {
        Task<IEnumerable<UserProfileViewModel>> FindUserProfiles(Guid userId);
    }
}
