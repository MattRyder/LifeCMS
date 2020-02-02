using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Socialite.WebAPI.Application.Commands.Albums
{
    public class UploadPhotoCommand : IRequest<bool>
    {
        public Guid AlbumId { get; private set; }

        public string Name { get; private set; }

        public IFormFile File { get; private set; }

        public string Caption { get; private set; }

        public UploadPhotoCommand(Guid albumId, string name, IFormFile file, string caption)
        {
            AlbumId = albumId;
            Name = name;
            File = file;
            Caption = caption;
        }
    }
}