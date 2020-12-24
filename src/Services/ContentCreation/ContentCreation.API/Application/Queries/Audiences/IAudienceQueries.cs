using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Audiences
{
    public interface IAudienceQueries
    {
        Task<IEnumerable<AudienceViewModel>> FindAudiences(Guid userId);

        Task<IEnumerable<SubscriberViewModel>> FindAudienceSubscribers(Guid audienceId);
    }
}
