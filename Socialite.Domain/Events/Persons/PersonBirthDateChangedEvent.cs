using Socialite.Domain.AggregateModels.PersonAggregate;
using Socialite.Domain.Common;

namespace Socialite.Domain.Events.Persons
{
    public class PersonBirthDateChangedEvent : BaseEvent
    {
        public Person Person { get; private set; }

        public PersonBirthDateChangedEvent(Person Person)
        {
            this.Person = Person;
        }
    }
}