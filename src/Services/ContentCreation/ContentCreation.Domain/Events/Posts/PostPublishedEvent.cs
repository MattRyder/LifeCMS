using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.Events.Posts
{
    /// <summary>
    /// Raised when a Post is published publically by a user
    /// </summary>
    public class PostPublishedEvent : BaseEvent
    {
        public Post Post { get; private set; }

        public PostPublishedEvent(Post publishedPost)
        {
            Post = publishedPost;
        }
    }
}