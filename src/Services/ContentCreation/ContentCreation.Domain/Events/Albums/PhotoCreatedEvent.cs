using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.Events.Albums
{
    public class PhotoCreatedEvent : BaseEvent
    {
        public Photo Photo { get; private set; }

        public PhotoCreatedEvent(Photo photo)
        {
            Photo = photo;
        }
    }
}
