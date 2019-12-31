using Socialite.Domain.AggregateModels.PersonAggregate;
using Socialite.Domain.Common;

namespace Socialite.Domain.Events.Persons
{
    public class PersonPhoneNumberChangedEvent : BaseEvent
    {
        public Person Person { get; private set; }

        public PersonPhoneNumberChangedEvent(Person Person)
        {
            this.Person = Person;
        }
    }
}