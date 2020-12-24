using System;
using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Audiences
{
    public class AudienceViewModel : ValueObject
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public static AudienceViewModel FromModel(Audience audience)
        {
            return new AudienceViewModel()
            {
                Id = audience.Id,
                UserId = audience.UserId,
                Name = audience.Name,
                CreatedAt = audience.CreatedAt,
                UpdatedAt = audience.UpdatedAt,
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return UserId;
            yield return Name;
            yield return CreatedAt;
            yield return UpdatedAt;
        }
    }
}
