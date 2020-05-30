using System;
using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Domain.Events.Albums;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate
{
    public class Album : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        private readonly List<Photo> _photos;
        public IReadOnlyCollection<Photo> Photos => _photos.AsReadOnly();

        protected Album()
        {
            _photos = new List<Photo>();
        }

        public Album(string name) : this()
        {
            Name = !string.IsNullOrEmpty(name) ? name : throw new AlbumDomainException(nameof(name));

            AddEvent(new AlbumCreatedEvent(this));
        }

        public void AddPhoto(string name, Uri location, string caption, int width, int height)
        {
            var photo = new Photo(name, location, caption, width, height);

            _photos.Add(photo);

            AddEvent(new PhotoCreatedEvent(photo));
        }
    }
}