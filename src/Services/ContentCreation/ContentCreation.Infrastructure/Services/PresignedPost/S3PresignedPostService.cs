using System;
using Amazon.S3;
using Amazon.S3.Model;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Services.Aws
{
    public class S3PresignedPostService : IPresignedPostService
    {
        private readonly IAmazonS3 _awsClient;

        private readonly string _bucketName;

        public S3PresignedPostService(IAmazonS3 awsClient, string bucketName)
        {
            _awsClient = awsClient;

            _bucketName = bucketName;
        }

        public string CreatePresignedUrl(string filename, string contentType)
        {
            var request = GenerateRequest(filename, contentType);

            return _awsClient.GetPreSignedURL(request);
        }

        public GetPreSignedUrlRequest GenerateRequest(
            string key,
            string contentType)
        {
            return new GetPreSignedUrlRequest()
            {
                BucketName = _bucketName,
                ContentType = contentType,
                Key = key,
                Expires = DateTime.UtcNow.AddMinutes(5),
                Verb = HttpVerb.PUT
            };
        }
    }
}
