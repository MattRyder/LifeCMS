using System;
using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Posts
{
    public class PostViewModel : ValueObject
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string State { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public static PostViewModel FromModel(Post post)
        {
            return new PostViewModel()
            {
                Id = post.Id,
                State = post.State.Name,
                Title = post.Title,
                Text = post.Text,
                CreatedAt = post.CreatedAt
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
