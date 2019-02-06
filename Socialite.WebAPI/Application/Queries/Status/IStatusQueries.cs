using System.Collections.Generic;
using System.Threading.Tasks;
using Socialite.Infrastructure.DTO;

namespace Socialite.WebAPI.Queries.Status
{
    public interface IStatusQueries
    {
        Task<IEnumerable<StatusDTO>> FindAllAsync();

        Task<StatusDTO> FindStatus(int id);
    }
}