using System;
using System.Collections.Generic;
using Bogus;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Infrastructure.DTO;

namespace Socialite.UnitTests.Factories
{
    public class PostFactory
    {
        public static Post Create()
        {
            return new Faker<Post>().CustomInstantiator(f =>
            {
                return new Post(f.Lorem.Paragraphs());
            });
        }

        public static List<Post> CreateList(int count = 0)
        {
            if (count <= 0)
            {
                count = new Random().Next(1, 20);
            }

            var postList = new List<Post>();

            for (var i = 0; i < count; i++)
            {
                postList.Add(Create());
            }

            return postList;
        }
    }
}