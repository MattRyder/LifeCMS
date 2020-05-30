using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LifeCMS.Services.ContentCreation.API.Infrastructure.Services
{
    public interface IImageUploadService
    {
        Task<Uri> UploadAsync(IFormFile imageFile);
    }
}