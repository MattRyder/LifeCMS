using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Respawn;
using Socialite.Domain.AggregateModels.AlbumAggregate;
using Socialite.Infrastructure.Data;
using Socialite.UnitTests.Factories;
using Socialite.WebAPI.Application.Queries.Albums;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Socialite.IntegrationTests.Application.Queries
{

    public class AlbumQueriesTest
    {
        private readonly Checkpoint _checkpoint;
        private readonly SocialiteDbContext dbContext;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public AlbumQueriesTest()
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

            dbContext = new SocialiteDbContext(contextOptions, null, null);

            dbContext.Database.EnsureDeleted();

            dbContext.Database.EnsureCreated();
        }

        [Fact]
        public void FindAllAsync_ReturnsOk()
        {
            RunWithDbContext(dbContext, async (context) =>
            {
                var albumsList = AlbumFactory.CreateList();

                context.Albums.Add(albumsList.First());

                await context.SaveChangesAsync();

                var albumViewModels = context.Set<Album>().ToList().ConvertAll<AlbumViewModel>(a => AlbumViewModel.FromModel(a));

                var albumQueries = new AlbumQueries(_dbConnectionFactory);

                var result = await albumQueries.FindAllAsync() as List<AlbumViewModel>;

                Assert.NotNull(result);

                Assert.Equal(albumViewModels, result);
            });
        }

        [Fact]
        public void FindAsync_ReturnsOk()
        {
            RunWithDbContext(dbContext, async (context) =>
            {
                var album = AlbumFactory.Create();

                context.Albums.Add(album);

                await context.SaveChangesAsync();

                var albumViewModel = AlbumViewModel.FromModel(album);

                var albumQueries = new AlbumQueries(_dbConnectionFactory);

                var result = await albumQueries.FindAsync(albumViewModel.Id) as AlbumViewModel;

                Assert.NotNull(result);

                Assert.Equal(albumViewModel, result);
            });
        }

        private void RunWithDbContext(SocialiteDbContext dbContext, Action<SocialiteDbContext> assertFunc)
        {
            _checkpoint.Reset(_dbConnectionFactory.CreateConnection().ConnectionString);

            assertFunc(dbContext);
        }
    }
}