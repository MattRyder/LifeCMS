using Socialite.Domain.AggregateModels.AlbumAggregate;
using Socialite.UnitTests.Factories;
using Xunit;
using Bogus;
using System;

namespace Socialite.UnitTests.Domain
{
    public class PhotoEntityTest
    {
        private readonly Faker _faker;

        public PhotoEntityTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Constructor_ShouldCreatePhoto_GivenValidParams()
        {
            var album = PhotoFactory.Create();

            Assert.NotNull(album);
        }

        [Fact]
        public void Constructor_ThrowsException_GivenInvalidName()
        {
            var photo = PhotoFactory.Create();

            Assert.Throws<AlbumDomainException>(() => new Photo(null, photo.Location, photo.Caption, photo.Width, photo.Height));
        }

        [Fact]
        public void Constructor_ThrowsException_GivenInvalidUri()
        {
            var photo = PhotoFactory.Create();

            Assert.Throws<AlbumDomainException>(() => new Photo(photo.Name, null, photo.Caption, photo.Width, photo.Height));
        }

        [Fact]
        public void Constructor_ThrowsException_GivenInvalidCaption()
        {
            var photo = PhotoFactory.Create();

            Assert.Throws<AlbumDomainException>(() => new Photo(photo.Name, photo.Location, null, photo.Width, photo.Height));
        }

        [Fact]
        public void Constructor_ThrowsException_GivenInvalidWidth()
        {
            var photo = PhotoFactory.Create();

            Assert.Throws<AlbumDomainException>(() => new Photo(photo.Name, photo.Location, photo.Caption, 0, photo.Height));
        }

        [Fact]
        public void Constructor_ThrowsException_GivenInvalidHeight()
        {
            var photo = PhotoFactory.Create();

            Assert.Throws<AlbumDomainException>(() => new Photo(photo.Name, photo.Location, photo.Caption, photo.Width, 0));
        }

        [Fact]
        public void SetName_ShouldUpdateName_GivenValidName()
        {
            var newName = _faker.Lorem.Word();

            var photo = PhotoFactory.Create();

            photo.SetName(newName);

            Assert.Equal(newName, photo.Name);
        }

        [Fact]
        public void SetName_ThrowsException_GivenInvalidName()
        {
            var photo = PhotoFactory.Create();

            Assert.Throws<AlbumDomainException>(() => photo.SetName(null));
        }

        [Fact]
        public void SetLocation_ShouldUpdateLocation_GivenValidUri()
        {
            var newLocation = new Uri(_faker.Internet.Url());

            var photo = PhotoFactory.Create();

            photo.SetLocation(newLocation);

            Assert.Equal(newLocation, photo.Location);
        }

        [Fact]
        public void SetName_ThrowsException_GivenIvnalidName()
        {
            var photo = PhotoFactory.Create();

            Assert.Throws<AlbumDomainException>(() => photo.SetLocation(null));
        }

        [Fact]
        public void SetCaption_ShouldUpdateCaption_GivenValidString()
        {
            var newCaption = _faker.Lorem.Sentence();

            var photo = PhotoFactory.Create();

            photo.SetCaption(newCaption);

            Assert.Equal(newCaption, photo.Caption);
        }

        [Fact]
        public void SetCaption_ThrowsException_GivenInvalidName()
        {
            var photo = PhotoFactory.Create();

            Assert.Throws<AlbumDomainException>(() => photo.SetCaption(null));
        }

    }
}