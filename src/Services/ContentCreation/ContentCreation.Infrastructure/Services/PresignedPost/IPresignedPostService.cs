namespace LifeCMS.Services.ContentCreation.Infrastructure.Services
{
    public interface IPresignedPostService
    {
        PresignedPostRequest CreatePresignedUrl(string filename, string contentType);
    }
}
