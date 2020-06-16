using System;
using Microsoft.EntityFrameworkCore;
using Respawn;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;

namespace LifeCMS.Services.ContentCreation.IntegrationTests.Application.Queries
{
    public class QueryTestBase
    {
        protected readonly Checkpoint _checkpoint;
        protected readonly ContentCreationDbContext _dbContext;
        protected readonly IDbConnectionFactory _dbConnectionFactory;

        protected QueryTestBase()
        {
            var integrationConnectionString = Environment.GetEnvironmentVariable(
                "ConnectionStrings__LifeCMSIntegrationTests"
            );

            _checkpoint = new Checkpoint
            {
                SchemasToInclude = new[]
                {
                    "public"
                },
                DbAdapter = DbAdapter.MySql,
            };

            _dbConnectionFactory = new MySqlDbConnectionFactory(integrationConnectionString);

            var contextOptions = new DbContextOptionsBuilder<ContentCreationDbContext>()
            .UseMySql(_dbConnectionFactory.CreateConnection().ConnectionString)
            .Options;

            _dbContext = new ContentCreationDbContext(contextOptions, null);

            _dbContext.Database.EnsureDeleted();

            _dbContext.Database.Migrate();
        }

        protected void RunWithDbContext(ContentCreationDbContext dbContext, Action<ContentCreationDbContext> assertFunc)
        {
            _checkpoint.Reset(_dbConnectionFactory.CreateConnection().ConnectionString);

            assertFunc(dbContext);
        }
    }
}