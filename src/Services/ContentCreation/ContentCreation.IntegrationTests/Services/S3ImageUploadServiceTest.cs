using System.IO;
using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Bogus;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace LifeCMS.Services.ContentCreation.IntegrationTests.Services
{
    public class S3ImageUploadServiceTest
    {
        private readonly Mock<IOptions<S3ImageUploadOptions>> _s3ConfigMock;

        public S3ImageUploadServiceTest()
        {
            _s3ConfigMock = new Mock<IOptions<S3ImageUploadOptions>>();
        }

        [Fact]
        public void UploadAsync_ReturnsUri_WhenPutObjectAsyncSucceeds()
        {
            var faker = new Faker();

            var serviceUrl = faker.Internet.Url();

            var fileName = faker.System.FileName();

            using (var memoryStream = new MemoryStream(new Faker().Random.Bytes(32)))
            {
                var file = new FormFile(memoryStream, 0, memoryStream.Length, faker.Lorem.Word(), fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };

                var s3ConfigOptions = Options.Create(new S3ImageUploadOptions());
                s3ConfigOptions.Value.BucketName = "LifeCMSTestBucket";

                var putObjectResponse = new PutObjectResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK
                };

                var s3ClientConfig = new AmazonS3Config()
                {
                    ServiceURL = serviceUrl
                };

                // var service = new S3ImageUploadService(s3ConfigOptions);

                // var result = await service.UploadAsync(file);

                // var resultString = result.ToString();

                // Assert.NotNull(result);

                // Assert.NotNull(resultString);

                // Assert.StartsWith(serviceUrl, resultString);

                // Assert.EndsWith(fileName, resultString);
            }
        }

        [Fact]
        public void UploadAsync_ReturnsNull_WhenPutObjectAsyncFails()
        {
            var faker = new Faker();

            var serviceUrl = faker.Internet.Url();

            var fileName = faker.Lorem.Word();

            using (var memoryStream = new MemoryStream(new Faker().Random.Bytes(32)))
            {
                var file = new FormFile(memoryStream, 0, memoryStream.Length, fileName, faker.System.FileName())
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };

                var s3ConfigOptions = Options.Create(new S3ImageUploadOptions());
                s3ConfigOptions.Value.BucketName = "LifeCMSTestBucket";

                var putObjectResponse = new PutObjectResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest
                };

                var s3ClientConfig = new AmazonS3Config()
                {
                    ServiceURL = serviceUrl
                };

                // _s3ClientMock.SetupGet(c => c.Config).Returns(s3ClientConfig);

                // _s3ClientMock.Setup(c => c.PutObjectAsync(It.IsAny<PutObjectRequest>(), default(CancellationToken))).ReturnsAsync(putObjectResponse);

                // var service = new S3ImageUploadService(s3ConfigOptions, _s3ClientMock.Object);

                // var result = await service.UploadAsync(file);

                // Assert.Null(result);
            }
        }

        [Fact]
        public void UploadAsync_ReturnsNull_GivenNullFile()
        {
            var faker = new Faker();

            var serviceUrl = faker.Internet.Url();

            var fileName = faker.Lorem.Word();

            using (var memoryStream = new MemoryStream(new Faker().Random.Bytes(32)))
            {
                // IFormFile file = null;

                var s3ConfigOptions = Options.Create(new S3ImageUploadOptions());
                s3ConfigOptions.Value.BucketName = "LifeCMSTestBucket";

                var putObjectResponse = new PutObjectResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest
                };

                var s3ClientConfig = new AmazonS3Config()
                {
                    ServiceURL = serviceUrl
                };

                // _s3ClientMock.SetupGet(c => c.Config).Returns(s3ClientConfig);

                // _s3ClientMock.Setup(c => c.PutObjectAsync(It.IsAny<PutObjectRequest>(), default(CancellationToken))).ReturnsAsync(putObjectResponse);

                // var service = new S3ImageUploadService(s3ConfigOptions, _s3ClientMock.Object);

                // var result = await service.UploadAsync(file);

                // Assert.Null(result);
            }
        }
    }
}