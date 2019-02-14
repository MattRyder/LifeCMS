using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Socialite.Infrastructure.DTO;

namespace Socialite.WebAPI.Queries.Status
{
    public class StatusQueries : IStatusQueries
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public StatusQueries(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<StatusDTO>> FindAllAsync()
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                string findAllQuery = @"
                    SELECT Id, Mood, Text, CreatedAt
                    FROM Statuses
                ";

                return await connection.QueryAsync<StatusDTO>(findAllQuery);
            }
        }

        public async Task<StatusDTO> FindStatus(int id)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                string findStatusQuery = @"
                    SELECT Id, Mood, Text, CreatedAt
                    FROM Statuses
                    WHERE Id = @StatusId
                ";

                var queryResult = await connection.QueryAsync<StatusDTO>(findStatusQuery, new { StatusId = id });

                if(queryResult.AsList().Count == 0)
                {
                    throw new KeyNotFoundException();
                }

                return queryResult.First();

            }
        }
    }
}