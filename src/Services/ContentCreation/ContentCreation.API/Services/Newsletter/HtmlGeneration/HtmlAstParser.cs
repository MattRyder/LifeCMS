using HtmlAgilityPack;

namespace LifeCMS.Services.ContentCreation.API.Services.Newsletter.HtmlGeneration
{
    public class HtmlAstParser
    {
        public CraftJsBody Body { get; private set; }

        public HtmlAstParser(CraftJsBody body)
        {
            Body = body;
        }

        public HtmlDocument Parse()
        {
            var doc = CreateHtmlDocument();

            var body = CreateBodyElement(doc);

            var rootNodeKey = "ROOT";

            var rootNode = Body.GetNode(rootNodeKey);

            var root = Parse(doc, rootNodeKey, rootNode);

            body.AppendChild(root);

            doc.DocumentNode.AppendChild(body);

            return doc;
        }

        private HtmlNode Parse(HtmlDocument doc, string nodeId, CraftJsNode node)
        {
            var divElem = doc.CreateElement("div");

            if (node.Props != null)
            {
                foreach (var attr in node.Props.GetAttributes())
                {
                    divElem.Attributes.Add(attr.Key, attr.Value);
                }

                if (!string.IsNullOrEmpty(node.Props.Text))
                {
                    divElem.InnerHtml = node.Props.Text;
                }
            }

            if (node.Nodes != null && node.Nodes.Count > 0)
            {
                foreach (var childNodeId in node.Nodes)
                {
                    var childNode = Body.GetNode(childNodeId);

                    if (childNode != null)
                    {
                        var elem = Parse(doc, childNodeId, childNode);

                        divElem.AppendChild(elem);
                    }
                }
            }

            return divElem;
        }

        private HtmlNode CreateBodyElement(HtmlDocument document)
        {
            return document.CreateElement("body");
        }

        private HtmlDocument CreateHtmlDocument()
        {
            var doc = new HtmlDocument();

            return doc;
        }
    }
}