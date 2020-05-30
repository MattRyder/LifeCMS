using System;
using Microsoft.EntityFrameworkCore;
using Respawn;
using Xunit;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;

namespace LifeCMS.Services.ContentCreation.IntegrationTests.Application.Queries
{
    public class PersonQueriesTest
    {
        private readonly Checkpoint _checkpoint;
        private readonly ContentCreationDbContext _dbContext;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public PersonQueriesTest()
        {
            var integrationConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__LifeCMSIntegrationTests");

            _checkpoint = new Checkpoint
            {
                SchemasToInclude = new[]
                {
                    "public"
                },
                DbAdapter = DbAdapter.MySql,
            };

            _dbConnectionFactory = new MySqlDbConnectionFactory(integrationConnectionString);

            var contextOptions = new DbContextOptionsBuilder<ContentCreationDbContext>().UseMySql(_dbConnectionFactory.CreateConnection().ConnectionString).Options;

            _dbContext = new ContentCreationDbContext(contextOptions, null);

            _dbContext.Database.EnsureDeleted();

            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public void FindAsync_ReturnsOk()
        {
            // RunWithDbContext(_dbContext, async (context) =>
            // {
            //     var person = PersonFactory.Create();

            //     context.Persons.Add(person);

            //     await context.SaveChangesAsync();

            //     var personViewModel = PersonViewModel.FromModel(person);

            //     var personQueries = new PersonQueries(_dbConnectionFactory);

            //     var result = await personQueries.FindAsync(person.Id) as PersonViewModel;

            //     Assert.NotNull(result);

            //     Assert.Equal(personViewModel, result);
            // });
        }

        [Fact]
        public void FindAsync_ThrowsException_GivenInvalidId()
        {
            // RunWithDbContext(_dbContext, async (context) =>
            // {
            //     var person = PersonFactory.Create();

            //     context.Persons.Add(person);

            //     await context.SaveChangesAsync();

            //     var personQueries = new PersonQueries(_dbConnectionFactory);

            //     await Assert.ThrowsAsync<KeyNotFoundException>(() => personQueries.FindAsync(999));
            // });
        }

        private void RunWithDbContext(ContentCreationDbContext dbContext, Action<ContentCreationDbContext> assertFunc)
        {
            _checkpoint.Reset(_dbConnectionFactory.CreateConnection().ConnectionString);

            assertFunc(dbContext);
        }
    }
}