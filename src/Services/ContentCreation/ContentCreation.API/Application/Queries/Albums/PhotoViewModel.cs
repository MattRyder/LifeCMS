using System;
using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Albums
{
    public class PhotoViewModel : ValueObject
    {
        public Guid Id { get; set; }
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Location { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public static PhotoViewModel FromModel(Photo photo)
        {
            return new PhotoViewModel()
            {
                Id = photo.Id,
                Name = photo.Name,
                Caption = photo.Caption,
                Location = photo.Location.ToString(),
                Width = photo.Width,
                Height = photo.Height
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Name;
            yield return Caption;
            yield return Location;
            yield return Width;
            yield return Height;
        }
    }
}
