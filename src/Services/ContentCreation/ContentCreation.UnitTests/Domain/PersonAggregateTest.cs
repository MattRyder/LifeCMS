using Bogus;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PersonAggregate;
using LifeCMS.Services.ContentCreation.UnitTests.Factories;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Domain
{
    public class PersonAggregateTest
    {
        [Fact]
        public void UpdatePhoneNumber_CreatesPhoneNumberChangedEvent_GivenValidPhoneNumber()
        {
            var newPhoneNumber = new Faker().Phone.PhoneNumber();

            var person = PersonFactory.Create();

            person.UpdatePhoneNumber(newPhoneNumber);

            Assert.Equal(newPhoneNumber, person.PhoneNumber);

            Assert.Equal(1, person.Events.Count);
        }

        [Fact]
        public void UpdatePhoneNumber_ThrowsException_GivenInvalidPhoneNumber()
        {
            var person = PersonFactory.Create();

            Assert.Throws<PersonDomainException>(() => person.UpdatePhoneNumber(null));
        }

        [Fact]
        public void UpdateBirthDate_CreatesBirthDateChangedEvent_GivenValidDate()
        {
            var newBirthDate = new Faker().Date.Past();

            var person = PersonFactory.Create();

            person.UpdateBirthDate(newBirthDate);

            Assert.Equal(newBirthDate, person.BirthDate);

            Assert.Equal(1, person.Events.Count);
        }

        [Fact]
        public void UpdateBirthDate_ThrowsException_GivenFutureDate()
        {
            var futureDate = new Faker().Date.Future();

            var person = PersonFactory.Create();

            Assert.Throws<PersonDomainException>(() => person.UpdateBirthDate(futureDate));
        }
    }
}