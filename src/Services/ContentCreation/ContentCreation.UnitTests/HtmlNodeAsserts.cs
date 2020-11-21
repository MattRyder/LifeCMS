using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Xunit.Sdk;

namespace LifeCMS.Services.ContentCreation.UnitTests
{
    public static class HtmlNodeAssert
    {
        private const string WhitespaceRegex = @"(?<=>)\s+?(?=<)|\r\n|\n";

        public static void HtmlEqual(string expectedHtml, HtmlNode node)
        {
            var expected = StripSpaces(expectedHtml);

            var actual = StripSpaces(node.OuterHtml);

            if (!expected.Equals(actual))
            {
                throw new EqualException(expected, actual);
            }
        }

        private static string StripSpaces(string source) =>
            new Regex(WhitespaceRegex)
                .Replace(source, "")
                .Trim();
    }
}
