using HtmlAgilityPack;

namespace LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration.HtmlTags
{
    public class TagRoot
    {
        private readonly HeadTag _headTag;

        private readonly BodyTag _bodyTag;

        public TagRoot(HeadTag headTag, BodyTag bodyTag)
        {
            _headTag = headTag;

            _bodyTag = bodyTag;
        }

        public HtmlDocument GetHtmlDocument()
        {
            return CreateDocument();
        }

        private HtmlDocument CreateDocument()
        {
            var document = new HtmlDocument();

            document.LoadHtml("<html></html>");

            var htmlNode = document.DocumentNode.SelectSingleNode("//html");

            htmlNode.AppendChild(_headTag.GetHtmlNode());

            htmlNode.AppendChild(_bodyTag.GetHtmlNode());

            return document;
        }
    }
}
