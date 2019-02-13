using System;
using System.Linq;
using System.Collections.Generic;
using Bogus;
using Socialite.Domain.AggregateModels.StatusAggregate;

namespace Socialite.UnitTests.Factories
{
    public class StatusFactory
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

        public static List<Status> CreateList(int count = 0)
        {
            if (count <= 0)
            {
                count = new Random().Next(1, 20);
            }

            var statusList = new List<Status>();

            for (var i = 0; i < count; i++)
            {
                statusList.Add(Create());
            }

            return statusList;
        }
    }
}