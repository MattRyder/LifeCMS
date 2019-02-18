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

        public static PostDTO CreateDTO()
        {
            return new Faker<PostDTO>()
                .RuleFor(p => p.CreatedAt, f => f.Date.Recent())
                .RuleFor(p => p.State, f => f.PickRandom(PostState.List()))
                .RuleFor(p => p.Text, f => f.Lorem.Paragraphs());
        }

        public static IEnumerable<Post> CreateList(int count = 0)
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

        public static IEnumerable<PostDTO> CreateDTOList(int count = 0)
        {
            if (count <= 0)
            {
                count = new Random().Next(1, 20);
            }

            var postList = new List<PostDTO>();

            for (var i = 0; i < count; i++)
            {
                postList.Add(CreateDTO());
            }

            return postList;
        }
    }
}