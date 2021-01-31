using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.UserProfiles
{
    public class UserProfileQueries : IUserProfileQueries
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UserProfileQueries(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<UserProfileViewModel>> FindUserProfiles(Guid userId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            var findUserProfileQuery = @"
                    SELECT  Id,
                            UserId,
                            Name,
                            EmailAddress,
                            Occupation,
                            Location,
                            Bio,
                            AvatarImageUrn,
                            HeaderImageUrn,
                            CreatedAt,
                            UpdatedAt
                    FROM UserProfiles
                    WHERE UserProfiles.UserId = @UserId
                ";

            var result = await connection.QueryAsync<UserProfileViewModel>(findUserProfileQuery, new { UserId = userId });

            return result;
        }
    }
}
