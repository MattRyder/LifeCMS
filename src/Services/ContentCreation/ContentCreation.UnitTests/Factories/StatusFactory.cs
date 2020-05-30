using System.Collections.Generic;
using Bogus;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.StatusAggregate;

namespace LifeCMS.Services.ContentCreation.UnitTests.Factories
{
    public class StatusFactory : FactoryBase<Status>
    {
        public static Status Create()
        {
            return new Faker<Status>().CustomInstantiator(
                f => new Status(
                    f.PickRandom(new[] { "👍", "👌", "🤘" }),
                    f.Lorem.Sentence()
                )
            );
        }

        public static IEnumerable<Status> CreateList() => MakeList(Create);
    }
}