using System;
using System.Threading.Tasks;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;

namespace LifeCMS.Services.ContentCreation.API.Services.Audiences
{
    public interface IAudienceLookupService
    {
        Task<Audience> FindAudienceAsync(Guid audienceId);
    }
}
