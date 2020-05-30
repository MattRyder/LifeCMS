using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PersonAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.Events.Persons
{
    public class PersonNameChangedEvent : BaseEvent
    {
        public Person Person { get; private set; }

        public PersonNameChangedEvent(Person person)
        {
            Person = person;
        }
    }
}