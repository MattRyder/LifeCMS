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
        private string _dbConnectionString;

        public StatusQueries(string dbConnectionString)
        {
            this._dbConnectionString = dbConnectionString;
        }

        public IDbConnection DbConnection
        {
            get { return new MySqlConnection(_dbConnectionString); }
        }

        public async Task<IEnumerable<StatusDTO>> FindAllAsync()
        {
            using (var connection = DbConnection)
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
            using (var connection = DbConnection)
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