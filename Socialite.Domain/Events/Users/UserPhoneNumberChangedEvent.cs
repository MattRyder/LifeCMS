using Socialite.Domain.AggregateModels.UsersAggregate;
using Socialite.Domain.Common;

namespace Socialite.Domain.Events.Users
{
    public class UserPhoneNumberChangedEvent : BaseEvent
    {
        public User User { get; private set; }

        public UserPhoneNumberChangedEvent(User user)
        {
            User = user;
        }
    }
}