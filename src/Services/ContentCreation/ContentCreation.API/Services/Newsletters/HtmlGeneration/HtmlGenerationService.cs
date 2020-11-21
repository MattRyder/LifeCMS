using System;
using LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration;

namespace LifeCMS.Services.ContentCreation.API.Services.Newsletters.HtmlGeneration
{
    public class HtmlGenerationService : IHtmlGenerationService
    {
        protected string Body { get; private set; }

        private CraftJsBody _craftJsBody;

        protected CraftJsBody CraftJsBody
        {
            get
            {
                if (_craftJsBody != null)
                {
                    return _craftJsBody;
                }
                else
                {
                    _craftJsBody = CraftJsBody.Parse(Body);

                    return _craftJsBody;
                }
            }
        }

        public HtmlGenerationService(string body)
        {
            Body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string GenerateHtml()
        {
            var craftJsBody = CraftJsBody.Parse(Body);

            var document = new HtmlTagParser(craftJsBody);

            return document.Parse().DocumentNode.InnerHtml;
        }
    }
}
