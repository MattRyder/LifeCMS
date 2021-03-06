using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LifeCMS.Services.ContentCreation.Infrastructure.Interfaces;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Albums
{
    public class AlbumQueries : IAlbumQueries
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public AlbumQueries(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<AlbumViewModel>> FindAllAsync()
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var findAllQuery = @"
                    SELECT Id, Name, CreatedAt, UpdatedAt
                    FROM Albums;";

                return await connection.QueryAsync<AlbumViewModel>(findAllQuery);
            }
        }

        public async Task<AlbumViewModel> FindAsync(Guid id)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var findByIdQuery = @"
                SELECT  Albums.Id, Albums.Name, Albums.CreatedAt, Albums.UpdatedAt,
                        Photos.Id, Photos.Name, Photos.AlbumId, Photos.Caption, Photos.Location, Photos.Height, Photos.Width
                FROM Albums LEFT JOIN
                Photos ON Photos.AlbumId = Albums.Id
                WHERE Albums.Id = @Id;";

                var albumLookup = new Dictionary<Guid, AlbumViewModel>();

                var result = await connection.QueryAsync<AlbumViewModel, PhotoViewModel, AlbumViewModel>(
                    findByIdQuery,
                    (album, photo) =>
                    {
                        AlbumViewModel albumViewModel;

                        if (!albumLookup.TryGetValue(album.Id, out albumViewModel))
                        {
                            albumLookup.Add(album.Id, album);

                            albumViewModel = album;
                        }

                        albumViewModel.Photos ??= new List<PhotoViewModel>();

                        if (photo != null)
                        {
                            albumViewModel.Photos.Add(photo);
                        }

                        return albumViewModel;
                    }, new { Id = id });

                result = result.Distinct();

                if (result.Count() == 0)
                {
                    throw new KeyNotFoundException();
                }

                return result.First();
            }
        }
    }
}
