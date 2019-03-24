using System;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Socialite.WebAPI.Infrastructure.Services
{
    public class S3ImageUploadService : IImageUploadService
    {
        private readonly IOptions<S3ImageUploadOptions> _s3Config;

        private readonly IAmazonS3 _s3Client;

        public S3ImageUploadService(IOptions<S3ImageUploadOptions> s3Config)
        {
            _s3Config = s3Config;

            _s3Client = new AmazonS3Client(
                new BasicAWSCredentials(_s3Config.Value.AccessKey, s3Config.Value.AccessSecret),
                new AmazonS3Config()
                {
                    RegionEndpoint = RegionEndpoint.GetBySystemName(_s3Config.Value.Region),
                    ServiceURL = _s3Config.Value.ServiceURL,
                    ForcePathStyle = true
                }
            );
        }

        public async Task<Uri> UploadAsync(IFormFile imageFile)
        {
            if (imageFile == null)
            {
                return null;
            }

            var putObjectRequest = GeneratePutObjectRequest(imageFile);

            PutObjectResponse response = await _s3Client.PutObjectAsync(putObjectRequest);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                var uriString = $"{_s3Client.Config.ServiceURL}/{_s3Config.Value.BucketName}/{putObjectRequest.Key}";

                return new Uri(uriString);
            }

            return null;
        }

        private PutObjectRequest GeneratePutObjectRequest(IFormFile file)
        {
            return new PutObjectRequest
            {
                BucketName = _s3Config.Value.BucketName,
                ContentType = file.ContentType,
                Key = GenerateObjectKey(file.FileName),
                InputStream = file.OpenReadStream()
            };
        }

        private string GenerateObjectKey(string fileName)
        {
            var dt = DateTime.Now;

            var year = dt.Year.ToString("d2");
            var month = dt.Month.ToString("d2");
            var day = dt.Day.ToString("d2");
            var hour = dt.Hour.ToString("d2");
            var minute = dt.Minute.ToString("d2");
            var second = dt.Second.ToString("d2");

            var datePart = $"{year}/{month}/{day}/{hour}/{minute}/{second}";
            var guidPart = Guid.NewGuid().ToString();

            return $"{datePart}/{guidPart}/{fileName}";
        }
    }
}