using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Audiences
{
    public class AudienceQueries : IAudienceQueries
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public AudienceQueries(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<AudienceViewModel>> FindAudiences(Guid userId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            var query = @"
            SELECT
                `Id`,
                `CreatedAt`,
                `UpdatedAt`,
                `UserId`,
                `Name`
            FROM  
                `Audiences`
            WHERE
                `Audiences`.`UserId` = @UserId;
            ";

            var result = await connection.QueryAsync<AudienceViewModel>(
                query,
                new { UserId = userId }
            );

            return result;
        }

        public async Task<IEnumerable<SubscriberViewModel>> FindAudienceSubscribers(Guid audienceId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            var query = @"
            SELECT
                `Id`,
                `CreatedAt`,
                `UpdatedAt`,
                `AudienceId`,
                `EmailAddress`,
                `Name`,
                `SubscribedAt`,
                `UnsubscribedAt`
            FROM  
                `Subscribers`
            WHERE
                `Subscribers`.`AudienceId` = @AudienceId;
            ";

            var result = await connection.QueryAsync<SubscriberViewModel>(
                query,
                new { AudienceId = audienceId }
            );

            return result;
        }
    }
}
