using System.Threading.Tasks;

namespace Socialite.WebAPI.Application.Queries.Users
{
    public interface IUserQueries
    {
        Task<UserViewModel> FindAsync(int userId);
    }
}