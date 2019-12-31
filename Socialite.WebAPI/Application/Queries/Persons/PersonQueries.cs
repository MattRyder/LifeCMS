using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Socialite.WebAPI.Application.Queries.Persons
{
    public class UserQueries : IPersonQueries
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UserQueries(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<PersonViewModel> FindByEmailAddressAsync(string emailAddress)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                string findPersonQuery = @"
                    SELECT Users.Id, Users.Email, Users.Name, Users.CreatedAt, Users.UpdatedAt
                    FROM Users
                    WHERE Users.Email = @EmailAddress;
                ";

                var result = await connection.QueryAsync<PersonViewModel>(findPersonQuery, new { EmailAddress = emailAddress });

                if (result.Count() == 0)
                {
                    throw new KeyNotFoundException();
                }

                return result.First();
            }
        }
    }
}