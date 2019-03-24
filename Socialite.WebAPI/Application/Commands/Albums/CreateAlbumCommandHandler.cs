using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Socialite.Domain.AggregateModels.AlbumAggregate;

namespace Socialite.WebAPI.Application.Commands.Albums
{
    public class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand, bool>
    {
        private readonly IAlbumRepository _albumRepository;

        public CreateAlbumCommandHandler(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public Task<bool> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var album = new Album(request.Name);

                _albumRepository.Add(album);

                return _albumRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (AlbumDomainException)
            {
                return Task.FromResult(false);
            }
        }
    }
}