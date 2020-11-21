using LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration.HtmlTags;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.HtmlGeneration.HtmlTags
{
    public class HeadTagTest
    {
        [Fact]
        public void ParseTextString_ReturnsCorrectHtmlNode()
        {
            var expected = @"       
            <head>
                <title></title>
                <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
            </head>";

            var tag = new HeadTag();

            var tagNode = tag.GetHtmlNode();

            HtmlNodeAssert.HtmlEqual(expected, tagNode);
        }
    }
}
