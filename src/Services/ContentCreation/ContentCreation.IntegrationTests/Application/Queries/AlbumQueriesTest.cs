using System.Collections.Generic;
using System.Linq;
using Xunit;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using LifeCMS.Services.ContentCreation.API.Application.Queries.Albums;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace LifeCMS.Services.ContentCreation.IntegrationTests.Application.Queries
{
    public class AlbumQueriesTest : QueryTestBase
    {
        [Fact]
        public void FindAllAsync_ReturnsOk()
        {
            RunWithDbContext(_dbContext, async (context) =>
            {
                var albumsList = AlbumFactory.CreateList();

                context.Albums.Add(albumsList.First());

                await context.SaveChangesAsync();

                var albumViewModels = context.Set<Album>().ToList().ConvertAll(AlbumViewModel.FromModel);

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

                var result = await albumQueries.FindAsync(albumViewModel.Id);

                Assert.NotNull(result);

                Assert.Equal(albumViewModel, result);
            });
        }
    }
}