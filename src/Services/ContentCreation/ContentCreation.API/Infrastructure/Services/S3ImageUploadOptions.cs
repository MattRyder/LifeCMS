namespace LifeCMS.Services.ContentCreation.API.Infrastructure.Services
{
    public class S3ImageUploadOptions
    {
        public string BucketName { get; set; }

        public string AccessKey { get; set; }

        public string AccessSecret { get; set; }

        public string Region { get; set; }

        public string ServiceURL { get; set; }
    }
}