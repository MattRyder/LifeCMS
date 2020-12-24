using System;
using Bogus;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;

namespace LifeCMS.Services.ContentCreation.UnitTests.Factories
{
    public class AudienceFactory : FactoryBase<Audience>
    {
        public static Audience Create(Guid userId)
        {
            return new Faker<Audience>().CustomInstantiator(f =>
            {
                return new Audience(userId, f.Lorem.Word());
            });
        }
    }
}
