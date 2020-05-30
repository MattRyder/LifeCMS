using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.Events.Posts
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