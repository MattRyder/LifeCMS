using System;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Domain.Events.Persons;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.PersonAggregate
{
    public class Person : BaseEntity, IAggregateRoot
    {
        public string Email { get; private set; }

        public string Name { get; private set; }

        public string PhoneNumber { get; private set; }

        public DateTime? BirthDate { get; private set; }

        public Person(string email, string name)
        {
            Email = !string.IsNullOrEmpty(email) ? email : throw new PersonDomainException(nameof(email));
            Name = !string.IsNullOrEmpty(name) ? name : throw new PersonDomainException(nameof(name));
        }

        public void UpdateName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new PersonDomainException(nameof(name));
            }

            Name = name;

            AddEvent(new PersonNameChangedEvent(this));
        }

        public void UpdatePhoneNumber(string phoneNumber)
        {
            if(string.IsNullOrEmpty(phoneNumber))
            {
                throw new PersonDomainException(nameof(phoneNumber));
            }

            PhoneNumber = phoneNumber;

            AddEvent(new PersonPhoneNumberChangedEvent(this));
        }

        public void UpdateBirthDate(DateTime birthDate)
        {
            if(birthDate.Subtract(DateTime.Now).Milliseconds > 0)
            {
                throw new PersonDomainException(nameof(birthDate));
            }

            BirthDate = birthDate;

            AddEvent(new PersonBirthDateChangedEvent(this));
        }
    }
}
