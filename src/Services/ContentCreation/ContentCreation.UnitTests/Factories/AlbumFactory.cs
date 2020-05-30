using System.Collections.Generic;
using Bogus;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;

namespace LifeCMS.Services.ContentCreation.UnitTests.Factories
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