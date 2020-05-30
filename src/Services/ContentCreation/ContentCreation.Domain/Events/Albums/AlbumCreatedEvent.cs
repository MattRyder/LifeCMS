using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.Domain.Events.Albums
{
    public class AlbumCreatedEvent : BaseEvent
    {
        public Album Album { get; private set; }

        public AlbumCreatedEvent(Album album)
        {
            Album = album;
        }
    }
}
