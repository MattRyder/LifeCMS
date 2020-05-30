using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Statuses
{
    public interface IStatusQueries
    {
        Task<IEnumerable<StatusViewModel>> FindAllAsync();

        Task<StatusViewModel> FindStatus(Guid id);
    }
}
