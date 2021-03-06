using System;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Domain.Events.Posts;
using LifeCMS.Services.ContentCreation.Domain.Exceptions;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate
{
    public class Post : BaseEntity, IAggregateRoot
    {
        /// <summary>
        /// GUID of the author of this Post
        /// </summary>
        /// <value></value>
        public Guid AuthorId { get; private set; }

        /// <summary>
        /// Title of this Post
        /// </summary>
        /// <value>This getter/setter returns a String</value>
        public string Title { get; private set; }

        /// <summary>
        /// The condition of this Post
        /// </summary>
        /// <value>This getter/setter returns an enumeration of PostState</value>
        public PostState State
        {
            get { return Enumeration.FromValue<PostState>(_stateId); }
            private set { _stateId = value.Id; }
        }
        private int _stateId;

        /// <summary>
        /// The textual content of this Post
        /// </summary>
        /// <value></value>
        public string Text { get; private set; }

        private Post()
        {
            _stateId = PostState.Drafted.Id;
        }

        public Post(Guid authorId, string title, string text) : this()
        {
            AuthorId = authorId != null ? authorId : throw new PostDomainException(nameof(authorId));

            Title = !string.IsNullOrEmpty(title) ? title : throw new PostDomainException(nameof(title));

            Text = !string.IsNullOrEmpty(text) ? text : throw new PostDomainException(nameof(text));
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
