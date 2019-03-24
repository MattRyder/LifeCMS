using System;
using System.Collections.Generic;
using Bogus;
using Socialite.Domain.AggregateModels.AlbumAggregate;

namespace Socialite.UnitTests.Factories
{
    public class PhotoFactory : FactoryBase<Photo>
    {
        public static Photo Create()
        {
            return new Faker<Photo>().CustomInstantiator(f =>
            {
                return new Photo(f.Lorem.Word(), new Uri(f.Internet.Url()), f.Lorem.Sentence(), f.Random.Number(Int32.MaxValue), f.Random.Number(Int32.MaxValue));
            });
        }

        public static IEnumerable<Photo> CreateList() => MakeList(Create);
    }
}