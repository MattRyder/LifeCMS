using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Socialite.WebAPI.Application.Queries.Statuses;

namespace Socialite.WebAPI.Queries.Statuses
{
    public class StatusQueries : IStatusQueries
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public StatusQueries(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<StatusViewModel>> FindAllAsync()
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var findAllQuery = @"
                    SELECT Id, Mood, Text, CreatedAt
                    FROM Statuses
                ";

                return await connection.QueryAsync<StatusViewModel>(findAllQuery);
            }
        }

        public async Task<StatusViewModel> FindStatus(Guid id)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var findStatusQuery = @"
                    SELECT Id, Mood, Text, CreatedAt
                    FROM Statuses
                    WHERE Id = @StatusId
                ";

                var queryResult = await connection.QueryAsync<StatusViewModel>(findStatusQuery, new { StatusId = id });

                if(queryResult.AsList().Count == 0)
                {
                    throw new KeyNotFoundException();
                }

                return queryResult.First();

            }
        }
    }
}