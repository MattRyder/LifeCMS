namespace LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration.HtmlTags
{
    public class DivTag : Tag
    {
        public DivTag() : base("div") { }

        public void SetText(string text)
        {
            _node.SelectSingleNode($"//{ElementName}").InnerHtml = text;
        }
    }
}
