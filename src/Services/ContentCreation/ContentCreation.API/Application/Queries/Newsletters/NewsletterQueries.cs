using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Newsletters
{
    public class NewsletterQueries : INewsletterQueries
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public NewsletterQueries(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<NewsletterViewModel>> FindNewsletters(Guid userId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            var query = @"
                SELECT 
                    Id,
                    UserId,
                    Name,
                    Body_DesignSource AS DesignSource,
                    Body_Html AS Html,
                    CreatedAt,
                    UpdatedAt
                FROM
                    Newsletters
                WHERE
                    Newsletters.UserId = @UserId;
            ";

            var result = await connection.QueryAsync<NewsletterViewModel>(
                query,
                new { UserId = userId }
            );

            return result;
        }
    }
}