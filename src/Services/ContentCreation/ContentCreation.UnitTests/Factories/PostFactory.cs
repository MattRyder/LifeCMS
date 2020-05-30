using System;
using System.Collections.Generic;
using Bogus;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;

namespace LifeCMS.Services.ContentCreation.UnitTests.Factories
{
    public class PostFactory : FactoryBase<Post>
    {
        public static Post Create()
        {
            return new Faker<Post>().CustomInstantiator(f =>
            {
                return new Post(new Guid(), f.Lorem.Sentence(), f.Lorem.Paragraphs());
            });
        }

        public static IEnumerable<Post> CreateList() => MakeList(Create);
    }
}