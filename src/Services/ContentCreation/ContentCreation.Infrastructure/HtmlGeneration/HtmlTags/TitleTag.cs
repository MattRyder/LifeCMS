namespace LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration.HtmlTags
{
    public class TitleTag : Tag
    {
        public TitleTag() : base("title") { }

        public void SetText(string text)
        {
            _node.SelectSingleNode($"//{ElementName}").InnerHtml = text;
        }
    }
}
