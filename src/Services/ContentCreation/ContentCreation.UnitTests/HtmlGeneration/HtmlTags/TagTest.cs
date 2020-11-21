using LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration.HtmlTags;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.HtmlGeneration.HtmlTags
{
    public class TagTest
    {
        [Fact]
        public void SetAttribute_ReturnsAttributeValue_GivenCorrectAttributeName()
        {
            var tag = new Tag("div");

            var expectedId = "test-id";

            tag.SetAttribute("id", expectedId);

            var actualId = tag.GetHtmlNode().GetAttributeValue("id", "");

            Assert.Equal(expectedId, actualId);
        }
    }
}
