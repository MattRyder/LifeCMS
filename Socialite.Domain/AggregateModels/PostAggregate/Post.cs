using System;
using Socialite.Domain.Common;
using Socialite.Domain.Events;
using Socialite.Domain.Exceptions;

namespace Socialite.Domain.AggregateModels.PostAggregate
{
    public class Post : BaseEntity, IAggregateRoot
    {
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Title of this Post
        /// </summary>
        /// <value>This getter/setter returns a String</value>
        public String Title { get; set; }

        /// <summary>
        /// The condition of this Post
        /// </summary>
        /// <value>This getter/setter returns an enumeration of PostState</value>
        private int? _stateId;
        public PostState State
        {
            get
            {
                return PostState.FromValue<PostState>(_stateId.Value);
            }
        }

        /// <summary>
        /// The textual content of this Post
        /// </summary>
        /// <value></value>
        public string Text { get; private set; }

        private Post()
        {
            _stateId = PostState.Drafted.Id;
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
            if (_stateId != PostState.Drafted.Id)
            {
                throw new PostDomainException("Cannot set Post state to published if not currently a draft");
            }

            _stateId = PostState.Published.Id;

            AddEvent(new PostPublishedEvent(this));
        }
    }
}