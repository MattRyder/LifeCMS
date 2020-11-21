using LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration.HtmlTags;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.HtmlGeneration.HtmlTags
{
    public class MetaTagTest
    {
        [Fact]
        public void GetHtmlNode_ReturnsCorrectHtmlNode()
        {
            var expected = @"<meta>";

            var tag = new MetaTag();

            var tagNode = tag.GetHtmlNode();

            HtmlNodeAssert.HtmlEqual(expected, tagNode);
        }

        [Fact]
        public void GetHtmlNode_ReturnCorrectNode_GivenAttributes()
        {
            var expected = @"<meta content=""text/html; charset=UTF-8"">";

            var tag = new MetaTag();

            tag.SetAttribute("content", "text/html; charset=UTF-8");

            var tagNode = tag.GetHtmlNode();

            HtmlNodeAssert.HtmlEqual(expected, tagNode);
        }
    }
}
