using System;
using System.Xml;

namespace LifeCMS.Services.ContentCreation.API.Services.Newsletter.HtmlGeneration
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

            var document = new HtmlAstParser(craftJsBody);

            return document.Parse().DocumentNode.InnerHtml;
        }
    }
}