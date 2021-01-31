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

        public PresignedPostRequest CreatePresignedUrl(
            string filename,
            string contentType)
        {
            var request = GenerateRequest(filename, contentType);

            return new PresignedPostRequest()
            {
                RequestUrl = _awsClient.GetPreSignedURL(request),
                RequestUrn = CreateFileUrn(request.Key),
            };
        }

        private string CreateFileUrn(string key)
        {
            var region = _awsClient.Config.RegionEndpoint.SystemName;

            return $"urn:lifecms:aws:s3:{region}:{_bucketName}:{key}";
        }

        private GetPreSignedUrlRequest GenerateRequest(
            string key,
            string contentType)
        {
            return new GetPreSignedUrlRequest()
            {
                BucketName = _bucketName,
                ContentType = contentType,
                Key = key,
                Expires = DateTime.UtcNow.AddMinutes(5),
                Verb = HttpVerb.PUT,
            };
        }
    }
}
