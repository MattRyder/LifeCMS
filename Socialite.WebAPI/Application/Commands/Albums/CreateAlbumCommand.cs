using MediatR;

namespace Socialite.WebAPI.Application.Commands.Albums
{
    public class CreateAlbumCommand : IRequest<bool>
    {
        public string Name { get; private set; }

        public CreateAlbumCommand(string name)
        {
            Name = name;
        }
    }
}