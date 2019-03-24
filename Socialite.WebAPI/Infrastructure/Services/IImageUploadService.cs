using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Socialite.WebAPI.Infrastructure.Services
{
    public interface IImageUploadService
    {
        Task<Uri> UploadAsync(IFormFile imageFile);
    }
}