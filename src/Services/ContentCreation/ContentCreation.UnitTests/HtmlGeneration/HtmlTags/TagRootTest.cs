using LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration.HtmlTags;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.HtmlGeneration.HtmlTags
{
    public class TagRootTest
    {
        [Fact]
        public void GetHtmlDocument_ReturnsHtmlDocument()
        {
            var expected = @"           
            <html>
                <head>
                    <title></title>
                    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                </head>
                <body></body>
            </html>";

            var tag = new TagRoot(new HeadTag(), new BodyTag());

            var doc = tag.GetHtmlDocument();

            HtmlNodeAssert.HtmlEqual(expected, doc.DocumentNode);
        }

        [Fact]
        public void GetHtmlDocument_ReturnsHtmlDocument_GivenChildNodes()
        {
            var expected = @"
            <html>
                <head>
                    <title></title>
                    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                </head>
                <body>
                    <div>Hello, World!</div>
                </body>
            </html>";

            var bodyTag = new BodyTag();

            var divTag = new DivTag();

            divTag.SetText("Hello, World!");

            bodyTag.AppendChild(divTag);

            var root = new TagRoot(new HeadTag(), bodyTag);

            var doc = root.GetHtmlDocument();

            HtmlNodeAssert.HtmlEqual(expected, doc.DocumentNode);
        }
    }
}
