using System;
using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.UserProfiles
{
    public class UserProfileViewModel : ValueObject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Name;
            yield return EmailAddress;
            yield return CreatedAt;
            yield return UpdatedAt;
        }

        public static UserProfileViewModel FromModel(UserProfile userProfile)
        {
            return new UserProfileViewModel
            {
                Id = userProfile.Id,
                Name = userProfile.Name,
                EmailAddress = userProfile.EmailAddress.Value,
                CreatedAt = userProfile.CreatedAt,
                UpdatedAt = userProfile.UpdatedAt
            };
        }
    }
}
