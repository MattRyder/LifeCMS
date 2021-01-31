using System;
using System.Text.RegularExpressions;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Data
{
    struct S3Urn
    {
        const string Pattern = "^urn:lifecms:aws:s3:(?<Region>[^:]+):(?<BucketName>[^:]+):(?<Key>[^:]+)$";

        public string Region { get; set; }

        public string BucketName { get; set; }

        public string Key { get; set; }

        public S3Urn(string urn)
        {
            var options = RegexOptions.Singleline | RegexOptions.CultureInvariant;

            var regex = new Regex(Pattern, options);

            var match = regex.Match(urn);

            if (!match.Success)
            {
                throw new FormatException(nameof(urn));
            }

            Region = match.Groups["Region"].Value;

            BucketName = match.Groups["BucketName"].Value;

            Key = match.Groups["Key"].Value;
        }
    };
}
