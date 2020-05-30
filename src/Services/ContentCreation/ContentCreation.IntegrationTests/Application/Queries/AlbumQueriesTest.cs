using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Respawn;
using Xunit;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Albums;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace LifeCMS.Services.ContentCreation.IntegrationTests.Application.Queries
{
    public class AlbumQueriesTest
    {
        private readonly Checkpoint _checkpoint;
        private readonly ContentCreationDbContext _dbContext;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public AlbumQueriesTest()
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
        public void FindAllAsync_ReturnsOk()
        {
            RunWithDbContext(_dbContext, async (context) =>
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
            RunWithDbContext(_dbContext, async (context) =>
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

        private void RunWithDbContext(ContentCreationDbContext dbContext, Action<ContentCreationDbContext> assertFunc)
        {
            _checkpoint.Reset(_dbConnectionFactory.CreateConnection().ConnectionString);

            assertFunc(dbContext);
        }
    }
}