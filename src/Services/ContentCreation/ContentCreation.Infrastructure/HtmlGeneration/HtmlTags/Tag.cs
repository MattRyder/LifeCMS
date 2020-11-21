using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration.HtmlTags
{
    public class Tag : ITag
    {
        protected HtmlNode _node;

        private readonly string _elementName;

        private readonly List<Tag> _children;

        public string ElementName
        {
            get { return _elementName; }
        }

        public Tag(string elementName)
        {
            _elementName = elementName;

            _children = new List<Tag>();

            _node = BuildNode();
        }

        public void AppendChild(Tag child)
        {
            _children.Add(child);
        }

        public List<Tag> GetChildren()
        {
            return _children;
        }

        public virtual HtmlNode GetHtmlNode()
        {
            var innerNode = _node.SelectSingleNode($"//{_elementName}");

            GetChildren().ForEach((child) =>
                innerNode.AppendChild(child.GetHtmlNode()));

            return innerNode;
        }

        private HtmlNode BuildNode()
        {
            var doc = new HtmlDocument();

            doc.LoadHtml($"<{_elementName}></{_elementName}>");

            return doc.DocumentNode;
        }

        public void SetAttribute(string name, string value)
        {
            var innerNode = _node.FirstChild;

            innerNode.Attributes.Add(name, value);
        }
    }
}
