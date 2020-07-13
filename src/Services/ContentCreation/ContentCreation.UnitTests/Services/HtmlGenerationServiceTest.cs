using LifeCMS.Services.ContentCreation.API.Services.Newsletter.HtmlGeneration;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Services.Newsletter.HtmlGeneration
{
    public class HtmlGenerationServiceTest
    {
        [Fact]
        public void ParseTextString_ReturnsJsonObject()
        {
            var body = "{\"ROOT\":{\"type\":\"div\",\"props\":{\"id\":\"row-1\",\"className\":\"page\"}}}";

            var expected = "<body><div classname=\"page\" id=\"row-1\" style=\"\"></div></body>";

            var result = new HtmlGenerationService(body).GenerateHtml();

            Assert.Equal(expected, result);
        }
    }
}