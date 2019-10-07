using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Socialite.Domain.AggregateModels.UsersAggregate;
using Socialite.Domain.Common;
using Socialite.Infrastructure.Data;

namespace Socialite.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public SocialiteDbContext DbContext { get; private set; }

        public IUnitOfWork UnitOfWork => DbContext;

        public UserRepository(SocialiteDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<User> FindAll()
        {
            return DbContext.Set<User>().ToList();
        }

        public ValueTask<User> FindAsync(int userId)
        {
            return DbContext.Set<User>().FindAsync(userId);
        }

        public User Add(User user)
        {
            return DbContext.Set<User>().Add(user).Entity;
        }

        public void Delete(User user)
        {
            DbContext.Set<User>().Remove(user);
        }
    }
}