using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Services;

namespace LifeCMS.Services.ContentCreation.API.Application.Commands.Albums
{
    public class UploadPhotoCommandHandler : IRequestHandler<UploadPhotoCommand, bool>
    {
        private readonly IAlbumRepository _albumRepository;

        private readonly IImageUploadService _imageUploadService;

        public UploadPhotoCommandHandler(IAlbumRepository albumRepository, IImageUploadService imageUploadService)
        {
            _albumRepository = albumRepository;

            _imageUploadService = imageUploadService;
        }

        public async Task<bool> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
        {
            var uploadLocationUri = await _imageUploadService.UploadAsync(request.File);

            if (uploadLocationUri != null)
            {
                var album = await _albumRepository.FindAsync(request.AlbumId);

                if (album == null)
                {
                    return false;
                }

                try
                {
                    album.AddPhoto(request.Name, uploadLocationUri, request.Caption, 1, 1);

                    return await _albumRepository.UnitOfWork.SaveEntitiesAsync();
                }
                catch (AlbumDomainException e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }

            }

            return await Task.FromResult(false);
        }
    }
}