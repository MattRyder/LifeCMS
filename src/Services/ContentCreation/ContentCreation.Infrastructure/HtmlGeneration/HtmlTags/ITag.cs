using System.Collections.Generic;
using HtmlAgilityPack;

namespace LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration.HtmlTags
{
    public interface ITag
    {
        HtmlNode GetHtmlNode();
    }
}
