using Socialite.Domain.AggregateModels.AlbumAggregate;
using Socialite.Domain.Common;

namespace Socialite.Domain.Events.Albums
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