using System;
using Socialite.Domain.Common;
using Socialite.Domain.Exceptions;

namespace Socialite.Domain.AggregateModels.PostAggregate
{
    public class Post : BaseEntity, IAggregateRoot
    {
        public DateTime CreatedAt { get; private set; }

        public PostState State { get; private set; }

        public string Text { get; private set; }

        public Post() { }

        public Post(string text) : this()
        {
            Text = !string.IsNullOrEmpty(text) ? text : throw new PostDomainException(nameof(text));
        }

        public static Post NewDraft()
        {
            var post = new Post();

            post.State = PostState.Drafted;

            return post;
        }


    }
}