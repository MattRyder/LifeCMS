using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.Common;

namespace Socialite.Domain.Events.Posts
{
    /// <summary>
    /// Event raised when a Post is initially drafted
    /// </summary>
    public class PostDraftedEvent : BaseEvent
    {
        public Post Post { get; private set; }

        public PostDraftedEvent(Post draftedPost)
        {
            Post = draftedPost;
        }
    }
}