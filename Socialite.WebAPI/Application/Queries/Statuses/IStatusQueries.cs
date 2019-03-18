using System.Collections.Generic;
using System.Threading.Tasks;
using Socialite.Infrastructure.DTO;
using Socialite.WebAPI.Application.Queries.Statuses;

namespace Socialite.WebAPI.Queries.Statuses
{
    public interface IStatusQueries
    {
        Task<IEnumerable<StatusViewModel>> FindAllAsync();

        Task<StatusViewModel> FindStatus(int id);
    }
}