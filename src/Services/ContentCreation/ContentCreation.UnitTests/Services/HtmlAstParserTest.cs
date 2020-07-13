using LifeCMS.Services.ContentCreation.API.Services.Newsletter.HtmlGeneration;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Services.Newsletter.HtmlGeneration
{
    public class HtmlAstParserTest
    {
        [Fact]
        public void Parse_ReturnsHtml_GivenEmptyBody()
        {
            var body = new CraftJsBody(new CraftJsObject
            {
                {
                    "ROOT",
                    new CraftJsNode()
                    {
                        Nodes = new[] { "asbCZU8Kd" },
                        Type = "div",
                        Props = new CraftJsProps()
                        {
                            ClassName = "page",
                            Id = "row-1",
                        }
                    }
                },
                {
                    "asbCZU8Kd",
                    new CraftJsNode()
                    {
                        Type = "div",
                        Props = new CraftJsProps()
                        {
                            Text = "Hello, World!",
                        }
                    }
                }
            }
            );

            var parser = new HtmlAstParser(body);

            var expected = "<body><div classname=\"page\" id=\"row-1\" style=\"\"><div style=\"\">Hello, World!</div></div></body>";

            var result = parser.Parse();

            var actual = result.DocumentNode.InnerHtml;

            Assert.Equal(expected, actual);
        }
    }
}