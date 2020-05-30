using System;
using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PersonAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Persons
{
    public class PersonViewModel : ValueObject
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        public static PersonViewModel FromModel(Person person)
        {
            return new PersonViewModel
            {
                Id = person.Id,
                Email = person.Email,
                Name = person.Name,
                CreatedAt = person.CreatedAt.ToString(),
                UpdatedAt = person.UpdatedAt.ToString()
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Email;
            yield return Name;
            yield return CreatedAt;
            yield return UpdatedAt;
        }
    }
}
