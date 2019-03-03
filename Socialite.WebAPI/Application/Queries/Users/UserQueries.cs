using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Socialite.WebAPI.Application.Queries.Users
{
    public class UserQueries : IUserQueries
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UserQueries(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<UserViewModel> FindAsync(int userId)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                string findUserQuery = @"
                    SELECT Users.Id, Users.Email, Users.Name, Users.CreatedAt, Users.UpdatedAt
                    FROM Users
                    WHERE Users.Id = @Id;
                ";

                var result = await connection.QueryAsync<UserViewModel>(findUserQuery, new { Id = userId });

                if(result.Count() == 0)
                {
                    throw new KeyNotFoundException();
                }

                return result.First();
            }
        }
    }
}