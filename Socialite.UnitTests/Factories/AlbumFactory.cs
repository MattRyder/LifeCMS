using System;
using System.Collections.Generic;
using Bogus;
using Socialite.Domain.AggregateModels.AlbumAggregate;

namespace Socialite.UnitTests.Factories
{
    public class AlbumFactory : FactoryBase<Album>
    {
        public static Album Create()
        {
            return new Faker<Album>().CustomInstantiator(f =>
            {
                return new Album(f.Lorem.Word());
            });
        }

        public static IEnumerable<Album> CreateList() => MakeList(Create);
    }
}