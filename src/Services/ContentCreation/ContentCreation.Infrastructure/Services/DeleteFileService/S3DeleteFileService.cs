using System.Net;
using System.Threading.Tasks;
using Amazon.S3;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Services.DeleteFileService
{
    public class S3DeleteFileService : IDeleteFileService
    {
        private readonly IAmazonS3 _s3Client;

        public S3DeleteFileService(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<bool> DeleteFileAsync(string urn)
        {
            var fileUrn = ParseUrn(urn);

            var response = await _s3Client.DeleteObjectAsync(
                fileUrn.BucketName,
                fileUrn.Key);

            return response.HttpStatusCode.Equals(HttpStatusCode.OK);
        }

        private S3Urn ParseUrn(string urn)
        {
            return new S3Urn(urn);
        }
    }
}
