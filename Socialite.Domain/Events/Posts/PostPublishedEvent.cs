using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.Common;

namespace Socialite.Domain.Events.Posts
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