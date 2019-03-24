using System;
using System.Collections.Generic;
using Bogus;
using Socialite.Domain.AggregateModels.PostAggregate;

namespace Socialite.UnitTests.Factories
{
    public class PostFactory : FactoryBase<Post>
    {
        public static Post Create()
        {
            return new Faker<Post>().CustomInstantiator(f =>
            {
                return new Post(f.Lorem.Sentence(), f.Lorem.Paragraphs());
            });
        }

        public static IEnumerable<Post> CreateList() => MakeList(Create);
    }
}