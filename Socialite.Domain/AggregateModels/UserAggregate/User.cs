using System;
using Socialite.Domain.Common;
using Socialite.Domain.Events.Users;

namespace Socialite.Domain.AggregateModels.UsersAggregate
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string Email { get; private set; }

        public string Name { get; private set; }

        public string PhoneNumber { get; private set; }

        public DateTime? BirthDate { get; private set; }

        public User(string email, string name)
        {
            Email = !string.IsNullOrEmpty(email) ? email : throw new UserDomainException(nameof(email));
            Name = !string.IsNullOrEmpty(name) ? name : throw new UserDomainException(nameof(name));
        }

        public void UpdateName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new UserDomainException(nameof(name));
            }

            Name = name;

            AddEvent(new UserNameChangedEvent(this));
        }

        public void UpdatePhoneNumber(string phoneNumber)
        {
            if(string.IsNullOrEmpty(phoneNumber))
            {
                throw new UserDomainException(nameof(phoneNumber));
            }

            PhoneNumber = phoneNumber;

            AddEvent(new UserPhoneNumberChangedEvent(this));
        }

        public void UpdateBirthDate(DateTime birthDate)
        {
            if(birthDate.Subtract(DateTime.Now).Milliseconds > 0)
            {
                throw new UserDomainException(nameof(birthDate));
            }

            BirthDate = birthDate;

            AddEvent(new UserBirthDateChangedEvent(this));
        }
    }
}