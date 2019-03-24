using System;
using Socialite.Domain.Common;

namespace Socialite.Domain.AggregateModels.AlbumAggregate
{
    public class Photo : BaseEntity
    {
        public string Name { get; private set; }

        public Uri Location { get; private set; }

        public string Caption { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Photo(string name, Uri location, string caption, int width, int height)
        {
            Name = !string.IsNullOrEmpty(name) ? name : throw new AlbumDomainException(nameof(name));

            Location = location != null && location.IsWellFormedOriginalString() ? location : throw new AlbumDomainException(nameof(location));

            Caption = !string.IsNullOrEmpty(caption) ? caption : throw new AlbumDomainException(nameof(caption));

            Width = width > 0 ? width : throw new AlbumDomainException(nameof(width));

            Height = height > 0 ? height : throw new AlbumDomainException(nameof(height));
        }

        public void SetName(string name)
        {
            Name = !string.IsNullOrEmpty(name) ? name : throw new AlbumDomainException(nameof(name));
        }

        public void SetLocation(Uri location)
        {
            Location = location != null && location.IsWellFormedOriginalString() ? location : throw new AlbumDomainException(nameof(location));
        }

        public void SetCaption(string caption)
        {
            Caption = !string.IsNullOrEmpty(caption) ? caption : throw new AlbumDomainException(nameof(caption));
        }
    }
}