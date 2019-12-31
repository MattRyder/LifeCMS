using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Respawn;
using Socialite.Infrastructure.Data;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Queries.Persons;
using Xunit;

namespace Socialite.IntegrationTests.Application.Queries
{
    public class PersonQueriesTest
    {
        private readonly Checkpoint _checkpoint;
        private readonly SocialiteDbContext dbContext;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public PersonQueriesTest()
        {
            var integrationConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__SocialiteIntegrationTests");

            _checkpoint = new Checkpoint
            {
                SchemasToInclude = new[]
                {
                    "public"
                },
                DbAdapter = DbAdapter.MySql,
            };

            _dbConnectionFactory = new MySqlDbConnectionFactory(integrationConnectionString);

            var contextOptions = new DbContextOptionsBuilder<SocialiteDbContext>().UseMySql(_dbConnectionFactory.CreateConnection().ConnectionString).Options;

            dbContext = new SocialiteDbContext(contextOptions, null);

            dbContext.Database.EnsureDeleted();

            dbContext.Database.EnsureCreated();
        }

        [Fact]
        public void FindAsync_ReturnsOk()
        {
            // RunWithDbContext(dbContext, async (context) =>
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
            // RunWithDbContext(dbContext, async (context) =>
            // {
            //     var person = PersonFactory.Create();

            //     context.Persons.Add(person);

            //     await context.SaveChangesAsync();

            //     var personQueries = new PersonQueries(_dbConnectionFactory);

            //     await Assert.ThrowsAsync<KeyNotFoundException>(() => personQueries.FindAsync(999));
            // });
        }

        private void RunWithDbContext(SocialiteDbContext dbContext, Action<SocialiteDbContext> assertFunc)
        {
            _checkpoint.Reset(_dbConnectionFactory.CreateConnection().ConnectionString);

            assertFunc(dbContext);
        }
    }
}