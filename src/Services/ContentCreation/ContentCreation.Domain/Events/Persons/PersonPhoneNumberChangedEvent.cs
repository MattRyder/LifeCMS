using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PersonAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.Events.Persons
{
    public class PersonPhoneNumberChangedEvent : BaseEvent
    {
        public Person Person { get; private set; }

        public PersonPhoneNumberChangedEvent(Person person)
        {
            Person = person;
        }
    }
}