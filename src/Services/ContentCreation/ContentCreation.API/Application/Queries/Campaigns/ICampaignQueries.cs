using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Campaigns
{
    public interface ICampaignQueries
    {
        Task<IEnumerable<CampaignViewModel>> FindCampaigns(Guid userId);
    }
}