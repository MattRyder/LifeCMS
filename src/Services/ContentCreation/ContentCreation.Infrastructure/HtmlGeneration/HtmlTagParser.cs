using System.Linq;
using HtmlAgilityPack;
using LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration.HtmlTags;

namespace LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration
{
    public class HtmlTagParser
    {
        private readonly CraftJsBody _body;

        public HtmlTagParser(CraftJsBody body)
        {
            _body = body;
        }

        public HtmlDocument Parse()
        {
            var head = CreateHeadTag();

            var body = CreateBodyTag();

            var rootNode = _body.GetNode("ROOT");

            body.AppendChild(ParseNode(rootNode));

            var tagRoot = new TagRoot(head, body);

            return tagRoot.GetHtmlDocument();
        }

        private Tag ParseNode(CraftJsNode node)
        {
            var tag = CreateTagForNodeType(node.Type);

            ParseProps(tag, node.Props);

            if (node.Nodes != null)
            {
                foreach (var childNodeId in node.Nodes)
                {
                    tag = ParseNodeChild(tag, childNodeId);
                }
            }

            return tag;
        }

        private static Tag ParseProps(Tag tag, CraftJsProps props)
        {
            if (tag == null || props == null)
            {
                return tag;
            }

            props.GetAttributes()
                .ToList()
                .ForEach((attr) => tag.SetAttribute(attr.Key, attr.Value));

            switch (tag)
            {
                case DivTag divTag:
                    if (!string.IsNullOrEmpty(props.Text))
                    {
                        divTag.SetText(props.Text);
                    }
                    break;
                case ImgTag imgTag:
                    if (!string.IsNullOrEmpty(props.Urn))
                    {
                        imgTag.SetSource(props.Urn);
                    }
                    break;
                default:
                    break;
            }

            return tag;
        }

        private Tag ParseNodeChild(Tag tag, string childNodeId)
        {
            if (!string.IsNullOrEmpty(childNodeId))
            {
                var childNode = _body.GetNode(childNodeId);

                if (childNode != null)
                {
                    var elem = ParseNode(childNode);

                    tag.AppendChild(elem);
                }
            }

            return tag;
        }

        private static Tag CreateTagForNodeType(string type)
        {
            return type switch
            {
                "Image" => CreateImgTag(),
                _ => CreateDivTag(),
            };
        }

        private static DivTag CreateDivTag() => new();

        private static BodyTag CreateBodyTag() => new();

        private static HeadTag CreateHeadTag() => new();

        private static ImgTag CreateImgTag() => new();
    }
}
