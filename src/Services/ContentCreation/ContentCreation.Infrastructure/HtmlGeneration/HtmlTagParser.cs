using System.Linq;
using HtmlAgilityPack;
using LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration.HtmlTags;

namespace LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration
{
    public class HtmlTagParser
    {
        public CraftJsBody Body { get; private set; }

        public HtmlTagParser(CraftJsBody body)
        {
            Body = body;
        }

        public HtmlDocument Parse()
        {
            var head = CreateHeadTag();

            var body = CreateBodyTag();

            var rootNode = Body.GetNode("ROOT");

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

        private Tag ParseProps(Tag tag, CraftJsProps props)
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
                    if (!string.IsNullOrEmpty(props.File.Url))
                    {
                        imgTag.SetSource(props.File.Url);
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
                var childNode = Body.GetNode(childNodeId);

                if (childNode != null)
                {
                    var elem = ParseNode(childNode);

                    tag.AppendChild(elem);
                }
            }

            return tag;
        }

        private Tag CreateTagForNodeType(string type)
        {
            return type switch
            {
                "Image" => CreateImgTag(),
                _ => CreateDivTag(),
            };
        }

        private DivTag CreateDivTag() => new DivTag();

        private BodyTag CreateBodyTag() => new BodyTag();

        private HeadTag CreateHeadTag() => new HeadTag();

        private ImgTag CreateImgTag() => new ImgTag();
    }
}
