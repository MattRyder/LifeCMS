using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using Xunit;
using Bogus;
using System;

namespace LifeCMS.Services.ContentCreation.UnitTests.Domain
{
    public class AlbumAggregateTest
    {
        [Fact]
        public void Constructor_ShouldCreateAlbum_GivenValidParams()
        {
            var album = AlbumFactory.Create();

            Assert.NotNull(album);

            Assert.NotNull(album.Photos);
        }

        [Fact]
        public void Constructor_ThrowsException_GivenInvalidName()
        {
            Assert.Throws<AlbumDomainException>(() => new Album(null));
        }

        [Fact]
        public void AddPhoto_ShouldCreatePhoto_GivenValidParams()
        {
            var album = AlbumFactory.Create();

            var photoParams = PhotoFactory.Create();

            album.AddPhoto(photoParams.Name, photoParams.Location, photoParams.Caption, photoParams.Width, photoParams.Height);

            Assert.Equal(1, album.Photos.Count);

            Assert.Equal(2, album.Events.Count);
        }

    }
}