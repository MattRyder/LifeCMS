using System.Threading.Tasks;

namespace LifeCMS.Services.ContentCreation.API.Application.Queries.Persons
{
    public interface IPersonQueries
    {
        Task<PersonViewModel> FindByEmailAddressAsync(string emailAddress);
    }
}
