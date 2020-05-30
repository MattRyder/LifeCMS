using System;
using System.Collections.Generic;
using System.Linq;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Albums
{
    public class AlbumViewModel : ValueObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<PhotoViewModel> Photos { get; set; }

        public AlbumViewModel()
        {
            Photos = new List<PhotoViewModel>();
        }

        public static AlbumViewModel FromModel(Album album)
        {
            return new AlbumViewModel()
            {
                Id = album.Id,
                Name = album.Name,
                Photos = album.Photos.ToList().ConvertAll<PhotoViewModel>(p => PhotoViewModel.FromModel(p))
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Name;

            foreach(var photo in Photos)
            {
                yield return photo;
            }
        }
    }
}
