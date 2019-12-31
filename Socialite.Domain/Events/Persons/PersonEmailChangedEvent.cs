using Socialite.Domain.AggregateModels.PersonAggregate;
using Socialite.Domain.Common;

namespace Socialite.Domain.Events.Persons
{
    public class PersonEmailChangedEvent : BaseEvent
    {
        public Person Person { get; private set; }

        public PersonEmailChangedEvent(Person Person)
        {
            this.Person = Person;
        }
    }
}