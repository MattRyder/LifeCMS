using System.Collections.Generic;

namespace LifeCMS.Services.ContentCreation.API.Services.Audiences
{
    public struct CallToAction
    {
        public string Link { get; set; }

        public string Text { get; set; }
    }

    public struct Paragraph
    {
        public string Title { get; set; }

        public string Text { get; set; }
    }

    public class SubscriberEmailViewModel
    {
        public string Title { get; set; }

        public string LeadText { get; set; }

        public string HeroImage { get; set; }

        public CallToAction CallToAction { get; set; }

        public IList<Paragraph> Paragraphs { get; set; }
    }
}
