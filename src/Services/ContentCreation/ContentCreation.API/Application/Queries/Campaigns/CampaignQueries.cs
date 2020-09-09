using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Campaigns
{
    public class CampaignQueries : ICampaignQueries
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CampaignQueries(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<CampaignViewModel>> FindCampaigns(Guid userId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            var query = @"
            SELECT
                `Id`,
                `CreatedAt`,
                `UpdatedAt`,
                `UserId`,
                `NewsletterTemplateId`,
                `UserProfileId`,
                `Name`,
                `Subject_SubjectLineText` AS `SubjectLineText`,
                `Subject_PreviewText` AS `PreviewText`,
                `ScheduledDate`,
                `UseSubscriberTimeZone`
            FROM  
                `Campaigns`
            WHERE
                `Campaigns`.`UserId` = @UserId;
            ";

            var result = await connection.QueryAsync<CampaignViewModel>(
                query,
                new { UserId = userId }
            );

            return result;
        }
    }
}