using System;
using System.Collections.Generic;
using Bogus;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;

namespace LifeCMS.Services.ContentCreation.UnitTests.Factories
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