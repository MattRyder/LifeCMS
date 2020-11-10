using System;
using DeepEqual.Syntax;
using LifeCMS.Services.ContentCreation.API.Services.Newsletters.HtmlGeneration;
using Xunit;

namespace LifeCMS.Services.ContentCreation.UnitTests.Services.Newsletter.HtmlGeneration
{
    public class CraftJsBodyTest
    {
        [Fact]
        public void Parse_ReturnsBody_GivenValidBodyString()
        {
            var body = "{\"ROOT\":{\"type\":\"div\",\"props\":{\"id\":\"row-1\",\"className\":\"page\"},\"nodes\":[\"asbCZU8Kd\",\"DY_62ecBN\"]}}";

            var expected = new CraftJsBody(new CraftJsObject
            {
                {
                    "ROOT",
                    new CraftJsNode()
                    {
                        Nodes = new[] { "asbCZU8Kd", "DY_62ecBN" },
                        Type = "div",
                        Props = new CraftJsProps()
                        {
                            ClassName = "page",
                            Id = "row-1",
                        }
                    }
                }
            }
            );

            var result = CraftJsBody.Parse(body);

            expected.ShouldDeepEqual(result);
        }

        [Fact]
        public void Parse_ReturnsBody_GivenValidTreeBodyString()
        {
            var body = "{\"ROOT\":{\"type\":\"div\",\"isCanvas\":true,\"props\":{\"id\":\"row-1\",\"className\":\"page\"},\"displayName\":\"de\",\"custom\":{},\"hidden\":false,\"nodes\":[\"qmPAj6B1c\"]},\"qmPAj6B1c\":{\"type\":{\"resolvedName\":\"Text\"},\"props\":{\"fontSize\":3,\"padding\":[1,1,1,1],\"text\":\"Hello, World!\"},\"displayName\":\"Text\",\"custom\":{},\"hidden\":false,\"parent\":\"ROOT\"}}";

            var expected = new CraftJsBody(
                new CraftJsObject()
            {
                {
                    "ROOT",
                    new CraftJsNode()
                    {
                        Nodes = new [] { "qmPAj6B1c" },
                        Props = new CraftJsProps()
                        {
                            Id = "row-1",
                            ClassName = "page"
                        },
                        Type = "div"
                    }
                },
                {
                    "qmPAj6B1c",
                    new CraftJsNode()
                    {
                        Props = new CraftJsProps()
                        {
                            ClassName = null,
                            Id = null,
                            FontSize = 3,
                            Padding = Tuple.Create(1, 1, 1, 1),
                            Text = "Hello, World!"
                        },
                        Type = "Text"
                    }
                }
            }
            );

            var result = CraftJsBody.Parse(body);

            expected.ShouldDeepEqual(result);
        }
    }
}
