using System;
using Socialite.Domain.Common;
using Socialite.Domain.Events;
using Socialite.Domain.Exceptions;

namespace Socialite.Domain.AggregateModels.PostAggregate
{
    public class Post : BaseEntity, IAggregateRoot
    {
        public DateTime CreatedAt { get; private set; }

        public PostState State { get; private set; }

        public string Text { get; private set; }

        private Post()
        {
            State = PostState.Drafted;
        }

        public Post(string text) : this()
        {
            Text = !string.IsNullOrEmpty(text) ? text : throw new PostDomainException(nameof(text));

            AddEvent(new PostDraftedEvent(this));
        }

        /// <summary>
        /// Update the Post entity to a State of Published
        /// </summary>
        public void SetPublishedState()
        {
            if(State != PostState.Drafted)
            {
                throw new PostDomainException("Cannot set Post state to published if not currently a draft");
            }

            State = PostState.Published;

            AddEvent(new PostPublishedEvent(this));
        }
    }
}