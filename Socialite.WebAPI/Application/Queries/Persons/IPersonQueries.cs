using System.Threading.Tasks;

namespace Socialite.WebAPI.Application.Queries.Persons
{
    public interface IPersonQueries
    {
        Task<PersonViewModel> FindByEmailAddressAsync(string emailAddress);
    }
}