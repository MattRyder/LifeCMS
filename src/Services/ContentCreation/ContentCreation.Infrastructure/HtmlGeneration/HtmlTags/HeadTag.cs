namespace LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration.HtmlTags
{
    public class HeadTag : Tag
    {
        public HeadTag() : base("head")
        {
            AppendChild(CreateTitleTag());

            AppendChild(CreateMetaContentTag());

            AppendChild(CreateMetaViewportTag());
        }

        private TitleTag CreateTitleTag() => new TitleTag();

        private MetaTag CreateMetaContentTag()
        {
            var metaTag = new MetaTag();

            metaTag.SetAttribute("http-equiv", "Content-Type");

            metaTag.SetAttribute("content", "text/html; charset=UTF-8");

            return metaTag;
        }

        private MetaTag CreateMetaViewportTag()
        {
            var metaTag = new MetaTag();

            metaTag.SetAttribute("name", "viewport");

            metaTag.SetAttribute("content", "width=device-width, initial-scale=1");

            return metaTag;


        }
    }
}
