using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PersonAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.Events.Persons
{
    public class PersonEmailChangedEvent : BaseEvent
    {
        public Person Person { get; private set; }

        public PersonEmailChangedEvent(Person person)
        {
            Person = person;
        }
    }
}