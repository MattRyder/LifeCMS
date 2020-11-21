using LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration.HtmlTags;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.HtmlGeneration.HtmlTags
{
    public class BodyTagTest
    {
        [Fact]
        public void GetHtmlNode_ReturnsCorrectHtmlNode()
        {
            var expected = @"<body></body>";

            var tag = new BodyTag();

            var tagNode = tag.GetHtmlNode();

            HtmlNodeAssert.HtmlEqual(expected, tagNode);
        }

        [Fact]
        public void GetHtmlNode_ReturnsCorrectHtmlNode_GivenTree()
        {
            var expected = @"<body><div>Hello, World!</div></body>";

            var tag = new BodyTag();

            var divTag = new DivTag();

            divTag.SetText("Hello, World!");

            tag.AppendChild(divTag);

            var tagNode = tag.GetHtmlNode();

            HtmlNodeAssert.HtmlEqual(expected, tagNode);
        }
    }
}
