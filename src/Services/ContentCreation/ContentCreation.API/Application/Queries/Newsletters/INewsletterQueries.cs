using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Newsletters
{
    public interface INewsletterQueries
    {
        Task<IEnumerable<NewsletterViewModel>> FindNewsletters(Guid userId);
    }
}
