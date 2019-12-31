using Socialite.Domain.AggregateModels.PersonAggregate;
using Socialite.Domain.Common;

namespace Socialite.Domain.Events.Persons
{
    public class PersonNameChangedEvent : BaseEvent
    {
        public Person Person { get; private set; }

        public PersonNameChangedEvent(Person Person)
        {
            this.Person = Person;
        }
    }
}