using Socialite.Domain.AggregateModels.UsersAggregate;
using Socialite.Domain.Common;

namespace Socialite.Domain.Events.Users
{
    public class UserNameChangedEvent : BaseEvent
    {
        public User User { get; private set; }

        public UserNameChangedEvent(User user)
        {
            User = user;
        }
    }
}