using System;
using Socialite.Domain.Common;
using Socialite.Domain.Events;
using Socialite.Domain.Events.Posts;
using Socialite.Domain.Exceptions;

namespace Socialite.Domain.AggregateModels.PostAggregate
{
    public class Post : BaseEntity, IAggregateRoot
    {
        /// <summary>
        /// Title of this Post
        /// </summary>
        /// <value>This getter/setter returns a String</value>
        public String Title { get; private set; }

        /// <summary>
        /// The condition of this Post
        /// </summary>
        /// <value>This getter/setter returns an enumeration of PostState</value>
        private int _stateId;
        public PostState State
        {
            get { return PostState.FromValue<PostState>(_stateId); }
            private set { _stateId = value.Id; }
        }

        /// <summary>
        /// The textual content of this Post
        /// </summary>
        /// <value></value>
        public string Text { get; private set; }

        private Post()
        {
            State = PostState.Drafted;
        }

        public Post(string title, string text) : this()
        {
            Title = !string.IsNullOrEmpty(title) ? title : throw new PostDomainException(nameof(title));
            Text = !string.IsNullOrEmpty(text) ? text : throw new PostDomainException(nameof(text));

            AddEvent(new PostDraftedEvent(this));
        }

        /// <summary>
        /// Update the Post entity to a State of Published
        /// </summary>
        public void SetPublishedState()
        {
            if (State != PostState.Drafted)
            {
                throw new PostDomainException("Cannot set Post state to published if not currently a draft");
            }

            State = PostState.Published;

            AddEvent(new PostPublishedEvent(this));
        }
    }
}