using System;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Services
{
    public interface IFileUriService
    {
        string GetFileUri(string fileUrn);
    }
}
