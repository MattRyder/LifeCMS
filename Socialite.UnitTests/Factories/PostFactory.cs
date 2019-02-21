using System;
using System.Linq;
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

        public static PostDTO CreateDTO()
        {
            return new Faker<PostDTO>()
                .StrictMode(true)
                .RuleFor(x => x.CreatedAt, f => f.Date.Recent())
                .RuleFor(x => x.Id, f => ++f.IndexFaker)
                .RuleFor(x => x.State, f => f.PickRandom(PostState.List()))
                .RuleFor(x => x.Text, f => f.Rant.Review());
        }

        public static IEnumerable<Post> CreateList() => MakeList(Create);

        public static IEnumerable<PostDTO> CreateDTOList() => MakeList(CreateDTO, 1);

        private static List<T> MakeList<T>(Func<T> createFunc, int count = 0)
        {
            if(count == 0)
            {
                count = new Random().Next(1, 20);
            }

            var list = new List<T>();

            for(var i = 0; i < count; i++)
            {
                list.Add(createFunc());
            }

            return list;
        }
    }
}