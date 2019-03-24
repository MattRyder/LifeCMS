using System;
using System.Linq;
using System.Collections.Generic;
using Bogus;
using Socialite.Domain.AggregateModels.StatusAggregate;

namespace Socialite.UnitTests.Factories
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