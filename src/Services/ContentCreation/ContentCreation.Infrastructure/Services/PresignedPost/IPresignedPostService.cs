namespace LifeCMS.Services.ContentCreation.Infrastructure.Services.Aws
{
    public interface IPresignedPostService
    {
        string CreatePresignedUrl(string filename, string contentType);
    }
}
