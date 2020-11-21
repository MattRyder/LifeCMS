namespace LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration.HtmlTags
{
    public class ImgTag : Tag
    {
        public ImgTag() : base("img") { }

        public void SetSource(string url)
        {
            SetAttribute("src", url);
        }

        public void SetAltText(string text)
        {
            SetAttribute("alt", text);
        }
    }
}
