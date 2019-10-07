using System.Collections.Generic;
using System.Threading.Tasks;
using Socialite.Domain.Interfaces;

namespace Socialite.Domain.AggregateModels.UsersAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> FindAll();

        ValueTask<User> FindAsync(int userId);

        User Add(User user);

        void Delete(User user);
    }
}