using LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.HtmlGeneration
{
    public class HtmlTagParserTest
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

            var parser = new HtmlTagParser(body);

            var expected = @"
            <html>
                <head>
                    <title></title>
                    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                </head>
                <body>
                    <div classname=""page"" id=""row-1"" style="""">
                    <div style="""">Hello, World!</div>
                    </div>
                </body>
            </html>
            ";

            var result = parser.Parse();

            HtmlNodeAssert.HtmlEqual(expected, result.DocumentNode);
        }
    }
}
