using Socialite.Domain.AggregateModels.AlbumAggregate;
using Socialite.Domain.Common;

namespace Socialite.Domain.Events.Albums
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