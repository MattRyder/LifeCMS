using System;
using System.Collections.Generic;
using System.Globalization;
using Socialite.Domain.AggregateModels.PostAggregate;

namespace Socialite.WebAPI.Queries.Posts
{
    public class PostViewModel : ValueObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string State { get; set; }
        public string Text { get; set; }
        public string CreatedAt { get; set; }

        public static PostViewModel FromModel(Post post)
        {
            return new PostViewModel()
            {
                Id = post.Id,
                State = post.State.Name,
                Title = post.Title,
                Text = post.Text,
                CreatedAt = post.CreatedAt.ToString()
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Title;
            yield return State;
            yield return Text;
            yield return CreatedAt;
        }
    }
}