using System;
using Amazon.S3;
using Amazon.S3.Model;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Services.Aws
{
    public class S3FileUriService : IFileUriService
    {
        private readonly IAmazonS3 _awsClient;

        public S3FileUriService(IAmazonS3 awsClient)
        {
            _awsClient = awsClient;
        }

        public string GetFileUri(string fileUrn)
        {
            var urn = new S3Urn(fileUrn);

            var request = GenerateRequest(urn);

            return _awsClient.GetPreSignedURL(request);
        }

        private GetPreSignedUrlRequest GenerateRequest(S3Urn urn)
        {
            return new GetPreSignedUrlRequest()
            {
                BucketName = urn.BucketName,
                Key = urn.Key,
                Expires = DateTime.UtcNow.AddMinutes(60),
                Verb = HttpVerb.GET,
            };
        }
    }
}
