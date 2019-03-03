using System;
using Bogus;
using Socialite.Domain.AggregateModels.UsersAggregate;
using Socialite.UnitTests.Factories;
using Xunit;

namespace Socialite.UnitTests.Domain
{
    public class UserAggregateTest
    {
        [Fact]
        public void Constructor_ReturnsUser_GivenValidEmail()
        {
            var email = new Faker().Internet.ExampleEmail();

            var name = new Faker().Name.FullName();

            var user = new User(email, name);

            Assert.NotNull(user);
        }

        [Fact]
        public void Constructor_ThrowsException_GivenInvalidEmail()
        {
            var name = new Faker().Name.FullName();

            Func<User> ctorWithNullEmail = () => new User(null, name);

            Assert.Throws<UserDomainException>(ctorWithNullEmail);
        }

        [Fact]
        public void Constructor_ThrowsException_GivenInvalidName()
        {
            var email = new Faker().Internet.ExampleEmail();

            Func<User> ctorWithNullName = () => new User(email, null);

            Assert.Throws<UserDomainException>(ctorWithNullName);
        }

        [Fact]
        public void UpdateName_CreatesNameChangedEvent_GivenValidName()
        {
            var newName = new Faker().Name.FullName();

            var user = UserFactory.Create();

            user.UpdateName(newName);

            Assert.Equal(newName, user.Name);

            Assert.Equal(1, user.Events.Count);
        }

        [Fact]
        public void UpdateName_ThrowsException_GivenInvalidName()
        {
            var user = UserFactory.Create();

            Assert.Throws<UserDomainException>(() => user.UpdateName(null));
        }

        [Fact]
        public void UpdatePhoneNumber_CreatesPhoneNumberChangedEvent_GivenValidPhoneNumber()
        {
            var newPhoneNumber = new Faker().Phone.PhoneNumber();

            var user = UserFactory.Create();

            user.UpdatePhoneNumber(newPhoneNumber);

            Assert.Equal(newPhoneNumber, user.PhoneNumber);

            Assert.Equal(1, user.Events.Count);
        }

        [Fact]
        public void UpdatePhoneNumber_ThrowsException_GivenInvalidPhoneNumber()
        {
            var user = UserFactory.Create();

            Assert.Throws<UserDomainException>(() => user.UpdatePhoneNumber(null));
        }

        [Fact]
        public void UpdateBirthDate_CreatesBirthDateChangedEvent_GivenValidDate()
        {
            var newBirthDate = new Faker().Date.Past();

            var user = UserFactory.Create();

            user.UpdateBirthDate(newBirthDate);

            Assert.Equal(newBirthDate, user.BirthDate);

            Assert.Equal(1, user.Events.Count);
        }

        [Fact]
        public void UpdateBirthDate_ThrowsException_GivenFutureDate()
        {
            var futureDate = new Faker().Date.Future();

            var user = UserFactory.Create();

            Assert.Throws<UserDomainException>(() => user.UpdateBirthDate(futureDate));
        }
    }
}