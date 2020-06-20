using System;
using Bogus;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;

namespace LifeCMS.Services.ContentCreation.UnitTests.Factories
{
    public class UserProfileFactory : FactoryBase<UserProfile>
    {
        public static UserProfile Create()
        {
            return new Faker<UserProfile>().CustomInstantiator(
                f => new UserProfile(
                    f.Random.Guid(),
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