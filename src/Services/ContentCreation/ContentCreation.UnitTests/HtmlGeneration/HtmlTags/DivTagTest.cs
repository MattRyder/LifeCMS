using LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration.HtmlTags;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.HtmlGeneration.HtmlTags
{
    public class DivTagTest
    {
        [Fact]
        public void GetHtmlNode_ReturnsCorrectHtmlNode()
        {
            var expected = @"<div></div>";

            var tag = new DivTag();

            var tagNode = tag.GetHtmlNode();

            HtmlNodeAssert.HtmlEqual(expected, tagNode);
        }

        [Fact]
        public void SetText_ReturnsCorrectHtmlNode_GivenText()
        {
            var text = "Hello, World!";

            var expected = "<div>Hello, World!</div>";

            var tag = new DivTag();

            tag.SetText(text);

            var tagNode = tag.GetHtmlNode();

            HtmlNodeAssert.HtmlEqual(expected, tagNode);
        }
    }
}
