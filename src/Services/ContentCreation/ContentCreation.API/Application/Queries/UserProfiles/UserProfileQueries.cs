using System;
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

        public async Task<UserProfileViewModel> FindUserProfile(Guid userId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            
            var findUserProfileQuery = @"
                    SELECT Id, Name, EmailAddress, CreatedAt, UpdatedAt
                    FROM UserProfiles
                ";

            return await connection.QueryFirstAsync<UserProfileViewModel>(findUserProfileQuery);
        }
    }
}
