using Socialite.Domain.AggregateModels.UsersAggregate;
using Socialite.Domain.Common;

namespace Socialite.Domain.Events.Users
{
    public class UserEmailChangedEvent : BaseEvent
    {
        public User User { get; private set; }

        public UserEmailChangedEvent(User user)
        {
            User = user;
        }
    }
}