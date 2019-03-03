using Socialite.Domain.AggregateModels.UsersAggregate;
using Socialite.Domain.Common;

namespace Socialite.Domain.Events.Users
{
    public class UserBirthDateChangedEvent : BaseEvent
    {
        public User User { get; private set; }

        public UserBirthDateChangedEvent(User user)
        {
            User = user;
        }
    }
}