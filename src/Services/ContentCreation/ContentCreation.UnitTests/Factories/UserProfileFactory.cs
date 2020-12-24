using System;
using System.Collections.Generic;
using Bogus;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.UnitTests.Factories
{
    public class UserProfileFactory : FactoryBase<UserProfile>
    {
        public static UserProfile Create(Guid? userId)
        {
            return new Faker<UserProfile>().CustomInstantiator(
                f => new UserProfile(
                    userId != null ? userId.Value : f.Random.Guid(),
                    f.Name.FullName(),
                    CreateEmailAddress(f),
                    f.Name.JobTitle(),
                    f.Address.City(),
                    f.Random.Words(4),
                    CreateImageUri(f),
                    CreateImageUri(f)
                )
            );
        }

        public static IEnumerable<UserProfile> CreateList(Guid? userId) => MakeList(
            delegate { return Create(userId); }
        );

        private static EmailAddress CreateEmailAddress(Faker faker)
        {
            return new EmailAddress(faker.Internet.Email());
        }

        private static Uri CreateImageUri(Faker faker)
        {
            return new Uri(faker.Image.LoremFlickrUrl());
        }
    }
}
